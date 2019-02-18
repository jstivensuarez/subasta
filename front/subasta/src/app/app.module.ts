import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
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
import { LoginComponent } from './login/login.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { JwtInterceptorService } from './services/autenticacion/jwt-interceptor.service';
import { JwtModule, JwtModuleOptions } from '@auth0/angular-jwt';
import { EventosModule } from './eventos/eventos.module';
import { SubastasModule } from './subastas/subastas.module';
import { LotesModule } from './lotes/lotes.module';
import { AnimalesModule } from './animales/animales.module';
import { PujadoresModule } from './pujadores/pujadores.module';
import {NgxMaterialTimepickerModule} from 'ngx-material-timepicker';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { SolicitudesModule } from './solicitudes/solicitudes.module';
import { MaestrosModule } from './maestros/maestros.module';
import { UsuariosModule } from './usuarios/usuarios.module';

export function getToken(){
  return localStorage.getItem('token');
}

const JWT_Module_Options: JwtModuleOptions = {
  config: {
      tokenGetter: getToken
  }
};

@NgModule({
  declarations: [
    AppComponent, ModalMessageComponent, DetallesComponent, LoginComponent, HeaderComponent, FooterComponent
  ],
  imports: [
  BrowserModule,
  DataTableModule, FormsModule, HttpClientModule, MaterialModule,
    RouterModule,
    NgbModule,
    AppRoutingModule,
    ClientesModule,
    MaterialModule,
    ReactiveFormsModule, 
    BrowserModule, 
    BrowserAnimationsModule,
    EventosModule,
    SubastasModule,
    LotesModule,
    AnimalesModule,
    PujadoresModule,
    SolicitudesModule,
    MaestrosModule,
    UsuariosModule,
    JwtModule.forRoot(JWT_Module_Options),
    [NgxMaterialTimepickerModule.forRoot()]
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpErrorInterceptor,
      multi: true,
    },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptorService, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
