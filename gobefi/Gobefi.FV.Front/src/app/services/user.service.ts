import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Plugins } from '@capacitor/core';
import { NavController } from '@ionic/angular';
import { environment } from 'src/environments/environment';

const { Storage } = Plugins;
const URL = environment.url;


@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(
    private http : HttpClient,
    private navCtrl: NavController
    ) { }

  
  token :  string = null;
  usuario : Usuario = {};
  menus : Menu[] = [];
  permisos : Permiso[] = [];
  escritura : boolean;
  lectura : boolean= false;
  isAdmin : boolean = false;
  modo : string ="Modo nocturno";
  icon : string = "moon"; 
  toogleValue : boolean;

 async setModoNocturno(){
    this.modo = "Modo nocturno";
    this.icon = "moon";
  }

 async setModoDiurno(){
    this.modo = "Modo diurno";
    this.icon = "sunny-outline";
  }

  login(email : string , password : string){

    const data = {email, password };

    return new Promise(resolve=>{
      this.http.post(`${URL}/account/login`,data)
      .subscribe(resp=>{
        //console.log(resp);
        if(resp['ok']){
          this.saveToken(resp['token']);
          this.getMenu();
          this.getPermisos();
          resolve(true);
        }else{
          this.token = null;
          Storage.clear();
          resolve(false);
        }
      });

    });
  }

  async saveToken(token: string){

    this.token = token;

    await Storage.set({
      key: 'token',
      value: token
    });
  }

  async loadToken(){
  
    const ret = await Storage.get({ key: 'token' });

    //console.log(ret);
    const token = ret.value;
    this.token = token || null;
  }

  async checkToken() : Promise<boolean>{
    //console.log("checkToken");
    await this.loadToken();
    if(!this.token){
      this.navCtrl.navigateRoot('/login');
      return Promise.resolve(false);
     
    }

    return new Promise<boolean>(resolve=>{
    const headers = new HttpHeaders({
    'Authorization' : "Bearer "+this.token
      });

      this.http.get(`${URL}/account/user`, {headers}).subscribe(resp=>{
        if(resp['ok']){

          this.usuario = resp['user'];
          this.getMenu();
          this.getPermisos();
          resolve(true);
        }else{
          this.navCtrl.navigateRoot('/login');
          resolve(false);

        }
      });
    });

  }

 async getUser(){
    
    if(!this.usuario.id){
      //console.log("0");
      await this.checkToken();
      //console.log(this.usuario);
    }
    //console.log("1");
    return {...this.usuario};
  }

  getMenu(){
    const headers = new HttpHeaders({
      'Authorization' : "Bearer "+this.token
    });
    return this.http.get<Menu[]>(`${URL}/menu`, {headers}).subscribe(resp=>{
      this.menus = resp;
      //console.log(this.menus);
    });
  }
  getPermisos(){
    if(this.usuario.role=="ADMINISTRADOR"){
      this.escritura = true;
      this.lectura = true;
      this.isAdmin = true;
      this.usuario.role="Administrador";
    }
    if(this.usuario.role=="GESTOR_SERVICIO"){
      this.usuario.role="Gestor de servicio";
      this.lectura = true;
      this.escritura = true;
    }
    if(this.usuario.role=="GESTOR_UNIDAD_FLOTA" || this.usuario.role=="GESTOR_FLOTA"){
      this.usuario.role="Gestor de flota";
      this.lectura = true;
      this.escritura = true;
    }
    
    //console.log(this.escritura);
  }

  async logout(){
    this.usuario = null;
    this.permisos=null;
    this.token = null;
    this.menus = null;
    await Storage.remove({
      key: 'token'
    });
    

  }

}
