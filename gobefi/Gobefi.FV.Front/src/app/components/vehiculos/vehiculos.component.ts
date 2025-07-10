import { Component, Input, OnInit, EventEmitter } from '@angular/core';
import { ModalController, AlertController } from '@ionic/angular';
import { ModalVehiculoComponent } from '../modal-vehiculo/modal-vehiculo.component';
import { VehiculosService } from '../../services/vehiculos.service';
import { FiltrosComponent } from '../filtros/filtros.component';

@Component({
  selector: 'app-vehiculos',
  templateUrl: './vehiculos.component.html',
  styleUrls: ['./vehiculos.component.scss'],
})
export class VehiculosComponent implements OnInit {

  @Input() pltWeb : boolean = false;
  @Input() vehiculos : Vehiculo[]=[];
  mensaje : string;
  habilitado = true;
  vehiculo : Vehiculo={};

  constructor(
    private modalController: ModalController,
    private vehiculoService:VehiculosService, 
    private alertController : AlertController
    ) { }

  ngOnInit() {
    this.vehiculos = [];
    this.next();
  }

  next(event?, pull:boolean = false){
    this.mensaje = "Cargando datos...";
    this.vehiculoService.getVehiculos(pull)
    .subscribe(resp=>{
      this.vehiculos.push(...resp);
      if(this.vehiculos.length===0){
        this.mensaje = "No hay datos para mostrar";
      }else{
        this.mensaje = "";
      }
      if(event&& event.target){
        event.target.complete();

        if(resp.length===0){
          this.habilitado = false;
        }
        
      }

    },(error)=>{
      this.mensaje = "No se pueden cargar los datos";
    })
  }

  recargar(event){
    
    this.habilitado = true;
    this.vehiculos = [];
    this.next(event, true);
  }


  async eliminarVehiculo(evt,id) {
    const alert = await this.alertController.create({
      cssClass: 'my-custom-class',
      header: 'Eliminar!!',
      message: '¿Está  seguro de eliminar el vehiculo',
      buttons: [
        {
          text: 'No',
          role: 'cancel',
          cssClass: 'secondary',
          handler: (blah) => {
            console.log('Confirm Cancel: blah');
          }
        }, {
          text: 'Si',
          cssClass: 'danger',
          handler: () => {
            console.log('Confirm Okay');
            this.vehiculoService.deleteVehiculo(id).subscribe(resp=>{
              console.log("Eliminado")
              this.vehiculos = [];
              this.next(null,true);
            })
          }
        }
      ]
    });

    await alert.present();
  }

  async editarVehiculo(vehiculoEvt,id){
    let eventEmitterEdit= new EventEmitter();
    //console.log(vehiculoEvt)
    eventEmitterEdit.subscribe(res=>{
      this.vehiculo = <Vehiculo> res;
      console.log("EventEmmiterEdit", this.vehiculo) 
      this.vehiculoService.updateVehiculo(id,this.vehiculo)
        .subscribe(res=>{
          this.vehiculos = [];
          this.next(null,true);
        })

    });

    const modal = await this.modalController.create({
      component: ModalVehiculoComponent,
      componentProps:{
        titulo: "Editar Vehiculo",
        vehiculo: vehiculoEvt,
        editVeiculo : eventEmitterEdit
      }
    });
    await modal.present();
  }

  
  async showModal(){
    let eventEmitter= new EventEmitter();
    eventEmitter.subscribe(res=>{
      this.vehiculo = <Vehiculo> res;
      console.log("emitterResult", this.vehiculo) 
      this.vehiculoService.postVehiculo(this.vehiculo)
        .subscribe(res=>{
          this.vehiculos = [];
          this.next(null,true);
        })
    });
    const modal = await this.modalController.create({
        component:ModalVehiculoComponent,
        componentProps:{
          titulo: "Nuevo Vehiculo",
          vehiculo: {},
          submitVeiculo: eventEmitter,
        }
      });
      await modal.present();
  }

  async showFilters(){

    let eventEmitter= new EventEmitter();
    eventEmitter.subscribe(res=>{
     
      console.log("emitterResult", res)
      this.vehiculoService.getVehiculosByFiltro(res)
      .subscribe(res=>{
        this.vehiculos = res;
        this.modalController.dismiss();
        this.habilitado = false;
      }) 
      

    });

    const modal = await this.modalController.create({
        component:FiltrosComponent,
        componentProps:{
          filtro: eventEmitter
        }
      });
       
      await modal.present();
  }

}
