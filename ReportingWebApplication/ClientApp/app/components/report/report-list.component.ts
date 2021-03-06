﻿import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { DataSource } from '@angular/cdk/collections';
import { MatSort, MatPaginator, MatDialog, MatSnackBar } from '@angular/material';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/startWith';
import 'rxjs/add/observable/merge';
import 'rxjs/add/observable/of';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/switchMap';
import 'rxjs/add/operator/toPromise';

import { ReportService } from "./report.service";
import { IReport } from "./report";
import { ShareDialogComponent } from '../shared/share-dialog.component';
import { IResponseResult, IEntityWithIdName, IListFilter } from '../shared/shared-interfaces';
import { Title } from '@angular/platform-browser';

import { saveAs } from 'file-saver';

@Component({
    selector: 'report',
    templateUrl: './report-list.component.html',
    styleUrls: ['./report-list.component.css', '../shared/shared-styles.css' ]
})
export class ReportListComponent implements OnInit {
    displayedColumns = ['Chart','Name', 'Query', 'Author', 'CreationDate', 'LastModifier', 'ModifyDate', 'Actions'];
    dataSource: ReportListDataSource | null;

    constructor(
        private service: ReportService,
        private dialog: MatDialog,
        private _snackbar: MatSnackBar,
        private titleService: Title) { }

    @ViewChild(MatSort) sort: MatSort;
    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild('filter') filter: ElementRef;

    ngOnInit() {
        this.titleService.setTitle("Reports");
        this.dataSource = new ReportListDataSource(this.service, this.sort, this.paginator);
        Observable.fromEvent(this.filter.nativeElement, 'keydown')
            .debounceTime(150)
            .distinctUntilChanged()
            .subscribe((k: any) => {
                if (!this.dataSource) { return; }
                if (k.which === 13)
                    this.dataSource.filter = this.filter.nativeElement.value;
            });
    }

    deleteReport(guid: string): void {
        this.service.deleteReport(guid)
            .subscribe(res => {
                this.dataSource!.filter = this.filter.nativeElement.value;
                this._snackbar.open(`Report deleted.`, 'x', {
                    duration: 5000
                });
            }, err => {
                this._snackbar.open(`Error: ${<any>err}`, 'x', {
                    duration: 5000
                });
            });
    }

    exportReport(reportGUID: string) {
        this.service.exportReport(reportGUID)
            .toPromise()
            .then(response => this.downloadFile(response));
    }

    downloadFile(response: Response) {
        let contentDispositionHeader: string = response.headers.get('Content-Disposition')!;
        const parts: string[] = contentDispositionHeader.split(';');
        const filename = parts[1].split('=')[1];
        const blob = new Blob([(<any>response)._body], { type: 'text/csv' });
        saveAs(blob, filename);
    }

    clearFilter(): void {
        this.filter.nativeElement.value = '';
        this.dataSource!.filter = '';
    }
}

export class ReportListDataSource extends DataSource<any> {
    totalCount = 0;
    isLoadingResults = false;

    _filterChange = new BehaviorSubject('');
    get filter(): string { return this._filterChange.value; }
    set filter(filter: string) { this._filterChange.next(filter); }

    constructor(private _reportService: ReportService, private _sort: MatSort, private _paginator: MatPaginator) {
        super();
    }

    connect(): Observable<IReport[]> {
        const displayDataChanges = [
            this._sort.sortChange,
            this._filterChange,
            this._paginator.page
        ];

        this._sort.sortChange.subscribe(() => this._paginator.pageIndex = 0);
        this._filterChange.subscribe(() => this._paginator.pageIndex = 0);

        return Observable.merge(...displayDataChanges)
            .startWith(null)
            .switchMap(() => {
                this.isLoadingResults = true;
                let filterObject: IListFilter = {
                    filter: this.filter,
                    page: this._paginator.pageIndex + 1,
                    sort: { columnName: this._sort.active, direction: this._sort.direction },
                    rows: this._paginator.pageSize,
                }
                return this._reportService.getReports(filterObject);
            })
            .map(data => {
                this.isLoadingResults = false;
                this.totalCount = data.totalCount;

                return data.reports;
            })
            .catch(() => {
                return Observable.of([]);
            });
    }

    disconnect() { }
}