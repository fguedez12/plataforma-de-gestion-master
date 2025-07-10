import { Injectable } from '@angular/core';
import { AlertController } from '@ionic/angular';

@Injectable({
  providedIn: 'root'
})
export class UiServiceService {

  constructor(
    private alertCtrl : AlertController
  ) { }

  async infoAlert(msj: string) {
    const alert = await this.alertCtrl.create({
      cssClass: 'my-custom-class',
     
      message: msj,
      buttons: ['OK']
    });

    await alert.present();
  }
}
