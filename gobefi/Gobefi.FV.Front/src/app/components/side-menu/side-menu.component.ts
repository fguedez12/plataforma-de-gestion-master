import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-side-menu',
  templateUrl: './side-menu.component.html',
  styleUrls: ['./side-menu.component.scss'],
})
export class SideMenuComponent implements OnInit {

  @Input() pltWeb : boolean;
  @Output() nuevoVehiculo = new  EventEmitter();
  @Output() filtros = new  EventEmitter();
  carrocerias : string[] = [];
  lectura : boolean = false;
  escritura : boolean= false;

  constructor(
    public modalController: ModalController,
    private userService : UserService
    ) { }

  ngOnInit() {
    this.escritura = this.userService.escritura;
  }

  showModal(){
    this.nuevoVehiculo.emit();
  }
  showFilters(){
    this.filtros.emit();
  }

}
