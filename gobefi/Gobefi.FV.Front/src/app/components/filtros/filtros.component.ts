import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { VehiculosService } from '../../services/vehiculos.service';
import { SectorPublicoService } from '../../services/sector-publico.service';
import { ModalController, PopoverController } from '@ionic/angular';
import { FormBuilder } from '@angular/forms';
import { PopoverModelosComponent } from '../popover-modelos/popover-modelos.component';

@Component({
  selector: 'app-filtros',
  templateUrl: './filtros.component.html',
  styleUrls: ['./filtros.component.scss'],
})
export class FiltrosComponent implements OnInit {

  @Output() filtro = new EventEmitter<any>();
  tipos : string[]=[];
  marcas : string[]=[];
  propulsiones : string[];
  ministerios: Institucion[];
  tipo : string ="";
  loadingTipo:boolean = false;
  loadingMarca:boolean = false;
  loadingPropulsiones : boolean = false;
  loadingMinisterios : boolean = false;

  filtroForm  = this.formBuilder.group({
    modelo: [''],
    modeloId:[''],
    ministerio:['']
  });
  constructor(
    private vehiculosService : VehiculosService,
    private sectorPublicoService : SectorPublicoService,
    private modalController : ModalController,
    public formBuilder: FormBuilder,
    private popoverController : PopoverController
    
  ) { }

  ngOnInit() {
    //this.loadingTipo = true;
    //this.loadingMarca = true;
    //this.loadingPropulsiones = true;
    
    // this.vehiculosService.getCarrocerias().subscribe(
    //   resp=>{
    //     this.loadingTipo = false;
    //     this.tipos = resp;

    //     this.vehiculosService.getMarcas().subscribe(
    //       resp=>{
    //         this.marcas = resp;
    //         this.loadingMarca = false;
    //         this.vehiculosService.getPropulsiones().subscribe(
    //           resp=>{
    //             this.loadingPropulsiones = false;
    //             this.propulsiones = resp;
    //           }
    //         );
    //       }
    //     );
    //   }
    // );
   
    this.loadingMinisterios = true;
    this.sectorPublicoService.getInstituciones().subscribe(
      resp=>{
        this.loadingMinisterios = false;
        this.ministerios = resp;
      }
    );
  }

  closeModal(){
    this.modalController.dismiss();
  }
  filtrar(){
    //console.log(this.filtroForm.value);
   this.filtro.emit(this.filtroForm.value);
  }

  async presentPopoverModelos(ev: any) {
    const popover = await this.popoverController.create({
      component: PopoverModelosComponent,
      cssClass: 'search-popover',
      event: ev,
      translucent: true
      
    });
    await popover.present();

    const { data } = await popover.onWillDismiss();
    if(data){
      this.filtroForm.patchValue({
        'modelo': `${data.marca}, ${data.modelo}`,
        'modeloId': data.id
      });
    }
    
    //console.log('vehiculo', data);
  }

}
