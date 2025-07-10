import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { UserService } from './user.service';

const URL = environment.url_sp;


@Injectable({
  providedIn: 'root'
})
export class SectorPublicoService {

  constructor(private http:HttpClient, private userService : UserService) { }

  usuario : Usuario = this.userService.usuario;

  getTiposPropiedad(){
    return this.http.get<Array<TipoPropiedad>>(`${URL}/tipopropiedad`);
  }

  getInstituciones(){
    return this.http.get<Array<Institucion>>(`${URL}/Instituciones`);
  }

  getInstitucionesById(){
    return this.http.get<Array<Institucion>>(`${URL}/Instituciones/getAsociadosByUserId/${this.usuario.id}`);
  }

  getServicios(institucionId : number)
  {
    //return this.http.get<Array<Servicio>>(`${URL}/servicios/getByInstitucionId/${institucionId}`);
    return this.http.get<Array<Servicio>>(`${URL}/servicios/getByInstitucionIdAndUsuarioId/${institucionId}/${this.usuario.id}`);
  }

  getRegiones()
  {
    return this.http.get<Array<Region>>(`${URL}/regiones/V2`);
  }

  getComunasByRegionId(id:number)
  {
    return this.http.get<Array<Region>>(`${URL}/comunas/V2/byRegionId/${id}`);
  }

}
