import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { UserService } from './user.service';

const URL = environment.url


@Injectable({
  providedIn: 'root'
})
export class VehiculosService {

  paginaVehiculos = 0;


  constructor(
    private http:HttpClient,
    private userService : UserService
    ) { }

  getVehiculos(pull: boolean = false){
    if(pull){
      this.paginaVehiculos =0;
    }
    const headers = new HttpHeaders({
      "Authorization" : "Bearer "+this.userService.token
    });
    this.paginaVehiculos ++;
    return this.http.get<Array<Vehiculo>>(`${URL}/vehiculos?page=${this.paginaVehiculos}`,{headers});
  }

  deleteVehiculo(id:number){
    var list  = new Array<patch>();
    var obj : patch = {op : "replace", path : "/Active", value : false};
    list.push(obj);
    return  this.http.patch(`${URL}/vehiculos/${id}`,list);
  }

  getVehiculosByFiltro(filtro ){
   
      let params = new HttpParams();
      params = params.append('modeloId', filtro.modeloId);
      params = params.append('ministerio', filtro.ministerio);
      return this.http.get<Vehiculo[]>(`${URL}/vehiculos/filtro`,{params : params});

  }

  getVehiculosByPatente(patente:string){
    
   
      let params = new HttpParams();
      params = params.append('patente', patente);
      return this.http.get<Vehiculo[]>(`${URL}/vehiculos/getbypatente`,{params : params});
    
    // this.paginaVehiculos=1;
    // return this.http.get<Array<Vehiculo>>(`${URL}/vehiculos?page=${this.paginaVehiculos}`);
   
  }

  getMarcas(){
    return this.http.get<Array<string>>(`${URL}/electromobilidad/marcas`);
  }

  getCarrocerias(){
    return this.http.get<Array<string>>(`${URL}/electromobilidad/carrocerias`);
  }
  getPropulsiones(){
    return this.http.get<Array<string>>(`${URL}/electromobilidad/propulsiones`);
  }
  buscarModelo(buscar : BuscarModelo ){
    return this.http.get<Array<Modelo>>(`${URL}/electromobilidad/buscar?marca=${buscar.marca}&propulsion=${buscar.propulsion}&carroceria=${buscar.carroceria}`);
  }

  postModelo(modelo)
  {
    return this.http.post<Modelo>(`${URL}/modelos`,modelo);
  }

  postVehiculo(vehiculo)
  {
    const headers = new HttpHeaders({
      "Authorization" : "Bearer "+this.userService.token
    });
    return this.http.post<Vehiculo>(`${URL}/vehiculos`,vehiculo,{headers});   
  }

  postVehiculoOtro(vehiculoOtro)
  {
    const headers = new HttpHeaders({
      "Authorization" : "Bearer "+this.userService.token
    });
    return this.http.post<VehiculoOtro>(`${URL}/vehiculos/otro`,vehiculoOtro,{headers});   
  }

  updateVehiculo(id,vehiculo){
    const headers = new HttpHeaders({
      "Authorization" : "Bearer "+this.userService.token
    });
    return this.http.put<Vehiculo>(`${URL}/vehiculos/${id}`,vehiculo,{headers});
  }

  postImage(file){
    const headers = new HttpHeaders({
      "Authorization" : "Bearer "+this.userService.token
    });
    return this.http.post<Imagen>(`${URL}/vehiculos/imagen`,file,{headers} ); 
  }

  deleteImage(id: string, url:string){
    const headers = new HttpHeaders({
      "Authorization" : "Bearer "+this.userService.token
    });
    // Initialize Params Object
    let params = new HttpParams();

    // Begin assigning parameters
    params = params.append('id', id);
    params = params.append('url', url);

    const options = { params: params, headers: headers };

    return this.http.delete(`${URL}/vehiculos/imagen`, options)
  }

}
