import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { DataTableModule } from 'angular-6-datatable';
import { HttpErrorInterceptor } from './services/autenticacion/errorInterceptor';
import { MaterialModule } from './material/material.module';
import { AppRoutingModule } from './app-routing.module';
import { ClientesModule } from './clientes/clientes.module';
import { ModalMessageComponent } from './modal-message/modal-message.component';
import { DetallesComponent } from './detalles/detalles.component';


@NgModule({
  declarations: [
    AppComponent, ModalMessageComponent, DetallesComponent
  ],
  imports: [
  BrowserModule,
  DataTableModule, FormsModule, HttpClientModule, MaterialModule,
    RouterModule,
    NgbModule,
    AppRoutingModule,
    ClientesModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpErrorInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
