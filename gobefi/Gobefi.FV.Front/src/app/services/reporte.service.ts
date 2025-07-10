import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { UserService } from './user.service';
import {FileTransfer, FileTransferObject} from '@ionic-native/file-transfer/ngx';
import { File } from '@ionic-native/file';


const URL = environment.url;


@Injectable({
  providedIn: 'root'
})
export class ReporteService {

 
  constructor(private http:HttpClient, 
    private userService : UserService
   
    ) { }

    

  getVehiculoReporte()
  {
    const headers = new HttpHeaders({
      'Authorization' : "Bearer "+this.userService.token,
    });
    return this.http.get(`${URL}/reportes/getreportevehiculos`, {headers,responseType: 'blob'});
  }
}
