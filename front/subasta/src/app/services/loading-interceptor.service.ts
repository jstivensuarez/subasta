import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpResponse } from '@angular/common/http';
import { LoadingService } from './loading.service';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class LoadingInterceptorService implements HttpInterceptor {

  constructor(private loaderService: LoadingService) { }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let ref = null;
    if(!req.url.includes("validateUser") && !req.url.includes("GetForClientAutenticated")){
        ref = this.showLoader();
    }
    return next.handle(req).pipe(tap((event: HttpEvent<any>) => {
      if (event instanceof HttpResponse) {
        this.onEnd(ref);
      }
    },
      (err: any) => {
        this.onEnd(ref);
      }));
  }
  
  private onEnd(ref): void {
    if(ref){
      this.hideLoader(ref);
    }  
  }
  private showLoader() {
    return this.loaderService.show();
  }
  private hideLoader(ref): void {
    this.loaderService.hide(ref);
  }

}
