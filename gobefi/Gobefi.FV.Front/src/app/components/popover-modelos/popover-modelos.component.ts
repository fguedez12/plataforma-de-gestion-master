import { Component, OnInit } from '@angular/core';
import { PopoverController } from '@ionic/angular';
import { ModeloService } from 'src/app/services/modelo.service';

@Component({
  selector: 'app-popover-modelos',
  templateUrl: './popover-modelos.component.html',
  styleUrls: ['./popover-modelos.component.scss'],
})
export class PopoverModelosComponent implements OnInit {

  modelos : ModeloSearch[] = [];

  constructor(
    private modeloService:ModeloService,
    private popoverController : PopoverController
    ) { }

  ngOnInit() {}
  
  changeSearchModelo(evt){
    console.log(evt.detail.value);
    if(evt.detail.value.length>2){
      this.modeloService.getModeloSearch(evt.detail.value)
      .subscribe(resp=>{
        //console.log(resp);
        this.modelos = resp;
      })
    }
  };

  selectVehiculo(vehiculo){
    this.popoverController.dismiss(vehiculo);
  };

}
