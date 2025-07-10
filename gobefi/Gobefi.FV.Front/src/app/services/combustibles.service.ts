import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { UserService } from './user.service';
const URL = environment.url

@Injectable({
  providedIn: 'root'
})
export class CombustiblesService {

  constructor(private http: HttpClient,private userService : UserService) { }

  getCombustibles()
  {
    const headers = new HttpHeaders({
      "Authorization" : "Bearer "+this.userService.token
    });
    return this.http.get<Propulsion[]>(`${URL}/combustibles`,{headers});
  }
}
