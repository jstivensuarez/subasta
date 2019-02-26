import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
  HttpResponse,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { constants } from 'src/app/util/constants';
import { environment } from 'src/environments/environment.prod';
import { local } from 'src/environments/environment';
import { MesaggesManagerService } from '../mesagges-manager.service';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { UsuarioService } from '../usuario.service';

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {

  constructor(private alertService: MesaggesManagerService,
              private router: Router,
              private usuarioService: UsuarioService) {

  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request)
      .pipe(
        retry(0),
        catchError((error: HttpErrorResponse) => {
          if (error.status === 401 && !error.url.includes('/login')) {
            this.usuarioService.logoutSession();
          }
          let errorMessage = '';
          if (error.error instanceof ErrorEvent) {
            // client-side error
            errorMessage = `Error: ${error.error.message}`;
          } else {
            // server-side error
            errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
          }
          if (error.error === constants.alreadyExist) {
            errorMessage = constants.alreadyExist;
          }
          if (error.error && error.error.innerException && error.error.innerException.message.includes(constants.enUso)) {
            errorMessage = constants.enUso;
          }
          if (error.error && error.error.innerException && error.error.innerException.message.includes(constants.subastaFinalizada)) {
            errorMessage = constants.subastaFinalizada;
          }
          return throwError(errorMessage);
        })
      )
  }
}
