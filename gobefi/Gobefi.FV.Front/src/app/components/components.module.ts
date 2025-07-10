import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VehiculosComponent } from './vehiculos/vehiculos.component';
import { VehiculoComponent } from './vehiculo/vehiculo.component';
import { IonicModule } from '@ionic/angular';
import { SideMenuComponent } from './side-menu/side-menu.component';
import { FiltrosComponent } from './filtros/filtros.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { MenuContentComponent } from './menu-content/menu-content.component';
import { PopoverModelosComponent } from './popover-modelos/popover-modelos.component';
import { FooterPageComponent } from './footer-page/footer-page.component';
import { RouterModule } from '@angular/router';
import { HeaderPageComponent } from './header-page/header-page.component';



@NgModule({
  declarations: [
    VehiculosComponent,
    VehiculoComponent,
    SideMenuComponent,
    FiltrosComponent,
    MenuContentComponent,
    PopoverModelosComponent,
    FooterPageComponent,
    HeaderPageComponent
 
  ],
  exports:[
    VehiculosComponent,
    SideMenuComponent,
    FiltrosComponent,
    MenuContentComponent,
    PopoverModelosComponent,
    FooterPageComponent,
    HeaderPageComponent
  ],
  imports: [
    CommonModule,
    IonicModule,
    FormsModule, 
    ReactiveFormsModule,
    RouterModule
  ]
})
export class ComponentsModule { }
