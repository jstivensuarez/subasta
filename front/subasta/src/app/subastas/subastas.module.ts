import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { CrearSubastaComponent } from './crear-subasta/crear-subasta.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '../material/material.module';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MaterialModule,
    FormsModule, ReactiveFormsModule, BrowserModule, BrowserAnimationsModule
  ],
  declarations: [CrearSubastaComponent],
  exports: [CrearSubastaComponent],
  providers:[DatePipe]
})
export class SubastasModule { }
