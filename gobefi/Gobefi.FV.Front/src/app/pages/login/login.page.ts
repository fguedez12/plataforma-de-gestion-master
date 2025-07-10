import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MenuController, NavController } from '@ionic/angular';
import { UiServiceService } from 'src/app/services/ui-service.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
})
export class LoginPage implements OnInit {

  constructor(
    
    private userService : UserService,
    private navCtrl : NavController,
    private menuCtrl : MenuController,
    private uiService : UiServiceService
  ) {

    this.menuCtrl.enable(false);

   }

  loginUser = {
    email:"",
    password:""
  }

  ngOnInit() {
    if(this.userService.checkToken()){
      this.navCtrl.navigateRoot('/home');
    }
    
  }

  async login(fLogin : NgForm){
    if(fLogin.invalid){return};
    const valid = await this.userService.login(this.loginUser.email,this.loginUser.password);

    if(valid){
      
      //navegar
      this.navCtrl.navigateRoot('/home');

    }else{
      //alerta
      this.uiService.infoAlert("Usuario o contraseña invalidos");
    }


    //console.log(this.loginUser);
  }

}
