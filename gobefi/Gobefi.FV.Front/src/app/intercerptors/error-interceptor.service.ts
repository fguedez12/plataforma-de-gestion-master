import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AlertController } from '@ionic/angular';
import { Observable, throwError } from 'rxjs';
import { catchError} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ErrorInterceptorService implements HttpInterceptor {

  constructor(private alertController: AlertController) { }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    
    const reqClone = req.clone({

    });

    return next.handle(reqClone).pipe(
      catchError((error: HttpErrorResponse) => {
        //console.log("Error:"+error);
        this.presentAlert(error.message,error.status);
        return throwError(error);
      })
    );
  }

  async presentAlert(msj,code) {
    const alert = await this.alertController.create({
      cssClass: 'my-custom-class',
      header: 'Error!',
      subHeader: 'Ocurrió un error, favor contactese con un administrador del sistema',
      message: `Codigo del error: ${code} - Detalle del error: ${msj}`,
      buttons: ['OK']
    });
    await alert.present();
  }
}
