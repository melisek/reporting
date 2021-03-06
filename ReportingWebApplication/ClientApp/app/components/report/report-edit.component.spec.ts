﻿import { TestBed, ComponentFixture } from "@angular/core/testing";
import { FormsModule, FormControl, ReactiveFormsModule, Validators, AbstractControl, FormBuilder, FormGroup, FormGroupDirective, FormControlDirective, NgControl } from '@angular/forms';

import { By } from "@angular/platform-browser";
import { DebugElement } from "@angular/core";
import { MaterialModule } from "../material/material-imports.module";
import { HttpModule } from "@angular/http";
import { AuthHttp } from "angular2-jwt";
import { Router, RouterModule, ActivatedRoute, ActivatedRouteSnapshot } from "@angular/router";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { ReportEditComponent } from "./report-edit.component";
import { ReportService } from "./report.service";
import { ChartModule } from "../chart/chart.module";
import { AuthModule } from "../user/auth.module";
import { QueryService } from "../query/query.service";
import { APP_BASE_HREF } from "@angular/common";
import { Observable } from "rxjs/Observable";


class RouterStub {
    navigate(params: any) {

    }
}
class ActivatedRouteStub {
    params: Observable<any> = new Observable<any>();
    snapshot: ActivatedRouteSnapshot = new ActivatedRouteSnapshot();
}

describe('ReportEditComponent tests', () => {
    let comp: ReportEditComponent;
    let fixture: ComponentFixture<ReportEditComponent>;
    let de: DebugElement;
    let el: HTMLElement;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [MaterialModule, FormsModule, HttpModule, BrowserAnimationsModule, AuthModule, RouterModule, ReactiveFormsModule, ChartModule],
            declarations: [ReportEditComponent],
            providers: [ReportService, QueryService, FormControlDirective, FormGroupDirective,
                { provide: Router, useClass: RouterStub },
                { provide: APP_BASE_HREF, useValue: '/' },
                { provide: ActivatedRoute, useClass: ActivatedRouteStub }]
        });

        fixture = TestBed.createComponent(ReportEditComponent);

        comp = fixture.componentInstance;

        fixture.detectChanges();
    });

    it('name control should be invalid on the form', () => {
        let control = comp.form.get('name');
        control!.setValue('');
        fixture.detectChanges();
        expect(control!.valid).toBeFalsy();
    });

    it('name control should be valid on the form', () => {
        let control = comp.form.get('name');
        control!.setValue('Jelentés 1');
        fixture.detectChanges();
        expect(control!.valid).toBeTruthy();
    });

    it('query control should be invalid on the form', () => {
        let control = comp.form.get('query');
        control!.setValue('');
        fixture.detectChanges();
        expect(control!.valid).toBeFalsy();
    });

    it('query control should be valid on the form', () => {
        let control = comp.form.get('query');
        control!.setValue('Query 1');
        fixture.detectChanges();
        expect(control!.valid).toBeTruthy();
    });

    it('save button should be disabled', () => {
        comp.columns.deselectAll();
        fixture.detectChanges();

        let de = fixture.debugElement.query(By.css('.saveButton'));
        expect(de.attributes['disabled']).toBeTruthy();
    });

    it('save button should not be disabled', () => {
        comp.columns.selectAll();
        fixture.detectChanges();

        let de = fixture.debugElement.query(By.css('.saveButton'));
        expect(de.attributes['disabled']).toBeTruthy();
    });


});


