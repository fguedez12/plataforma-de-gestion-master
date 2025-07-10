import { Component, DoCheck, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-menu-content',
  templateUrl: './menu-content.component.html',
  styleUrls: ['./menu-content.component.scss'],
})
export class MenuContentComponent implements OnInit,DoCheck {

  usuario : Usuario = {};
  menus : Menu[] = [];
  constructor(
    private userService : UserService
  ) { }


  ngDoCheck() {
    this.usuario = this.userService.usuario;
    this.menus = this.userService.menus;
  }

 async ngOnInit() {

  }

  logout(){
    this.userService.logout();
  }

}
