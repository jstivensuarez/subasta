import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { CrearSubastaComponent } from './crear-subasta/crear-subasta.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '../material/material.module';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CurrencyMaskModule } from 'ng2-currency-mask';
import { CustomCurrencyMaskConfig } from '../util/numberMaskConfig';
import { CURRENCY_MASK_CONFIG } from 'ng2-currency-mask/src/currency-mask.config';
import { SubastasComponent } from './subastas/subastas.component';
import { ListarSubastaComponent } from './listar-subasta/listar-subasta.component';
import { CrearSubastaNuevaComponent } from './crear-subasta-nueva/crear-subasta-nueva.component';
import { NgxMaterialTimepickerModule } from 'ngx-material-timepicker';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AppRoutingModule } from '../app-routing.module';
import { ClientesModule } from '../clientes/clientes.module';
import {CountDown} from "ng6-date-countdown";

@NgModule({
  imports: [
    CommonModule,
    MaterialModule,
    FormsModule, 
    ReactiveFormsModule, 
    BrowserModule, 
    BrowserAnimationsModule, 
    CurrencyMaskModule,
    [NgxMaterialTimepickerModule.forRoot()],
     HttpClientModule,
    RouterModule,
    NgbModule,
    AppRoutingModule,
  ],
  declarations: [CrearSubastaComponent, SubastasComponent, CountDown,
    ListarSubastaComponent, CrearSubastaNuevaComponent],
  exports: [CrearSubastaComponent],
  providers:[DatePipe,  
    { provide: CURRENCY_MASK_CONFIG, useValue: CustomCurrencyMaskConfig }]
})
export class SubastasModule { }
