//import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
//import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';


import {
    MdButtonModule, MdCheckboxModule, MdInputModule, MdCardModule, MdMenuModule, MdButtonToggleModule, MdSidenavModule, MdSortModule, MdTableModule, MdPaginatorModule
} from '@angular/material';


import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent
    ],
    imports: [
        //BrowserModule,
        BrowserAnimationsModule,    
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: '**', redirectTo: 'home' }
        ]),

        MdButtonModule,
        MdCheckboxModule,
        MdInputModule,
        MdCardModule,
        MdButtonToggleModule,
        MdMenuModule,
        MdSidenavModule,
        MdSortModule,
        MdTableModule,
        MdPaginatorModule
    ],
    exports: [
        MdButtonModule,
        MdCheckboxModule,
        MdInputModule,
        MdCardModule,
        MdButtonToggleModule,
        MdMenuModule,
        MdSidenavModule,
        MdSortModule,
        MdTableModule,
        MdPaginatorModule
    ]
})
export class AppModuleShared {
}
