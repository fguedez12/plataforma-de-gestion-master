import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

const URL = environment.url

@Injectable({
  providedIn: 'root'
})
export class ModeloService {

  constructor(
    private http:HttpClient
  ) { }

  getModeloSearch(filtro){
   
    let params = new HttpParams();
    params = params.append('filter', filtro);
    return this.http.get<ModeloSearch[]>(`${URL}/modelos/search`,{params : params});

  }
}
