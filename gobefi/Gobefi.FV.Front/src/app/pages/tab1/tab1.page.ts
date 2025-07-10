import { Component, Input, OnInit, EventEmitter, ViewChild } from '@angular/core';
import { MenuController, Platform } from '@ionic/angular';
import { VehiculosComponent } from '../../components/vehiculos/vehiculos.component';
import { VehiculosService } from '../../services/vehiculos.service';



@Component({
  selector: 'app-tab1',
  templateUrl: 'tab1.page.html',
  styleUrls: ['tab1.page.scss']
})
export class Tab1Page implements OnInit {

  @ViewChild('listadoVehiculos') listado : VehiculosComponent;
  vehiculos: Vehiculo[] = [];

  pltWeb : boolean = false;
  modo : string ="Modo nocturno";
  icon : string = "moon";
  title : string = "Mi flota vehicular" 
  constructor(
    private platform: Platform,
    private vehiculosService : VehiculosService,
    private menuCtrl : MenuController
    ) {
      this.menuCtrl.enable(true);
    }
    
  ngOnInit(){
    if(!this.platform.is('hybrid')){
      this.pltWeb = true;
     }
  }

  nuevoVehiculo(evt){
    this.listado.showModal();
  }

  filtros(evt){
    this.listado.showFilters();
  }

  onSearchChange(evt){
    console.log(evt.detail.value);
    this.vehiculosService.getVehiculosByPatente(evt.detail.value).subscribe(resp=>{
      //console.log(resp);
      this.vehiculos = resp;
    })
    
  }

}
