import { Component, Input, OnInit, Output, EventEmitter, DoCheck } from '@angular/core';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-vehiculo',
  templateUrl: './vehiculo.component.html',
  styleUrls: ['./vehiculo.component.scss'],
})
export class VehiculoComponent implements OnInit {


  @Input() vehiculo : Vehiculo ;
  @Output() editVehiculo= new EventEmitter<any>();
  @Output() deleteVehiculo = new EventEmitter<any>();
  lectura : boolean = false;
  escritura : boolean= false;

  constructor(private userService : UserService) { }


  ngOnInit() {
    this.escritura = this.userService.escritura;
    //console.log(this.escritura);
  }

  editar(){
    console.log(this.vehiculo);
    this.editVehiculo.emit(this.vehiculo);
  }

  eliminar(){
    this.deleteVehiculo.emit(this.vehiculo.id);
  }

}
