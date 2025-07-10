import { Component, OnInit, Input } from '@angular/core';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-header-page',
  templateUrl: './header-page.component.html',
  styleUrls: ['./header-page.component.scss'],
})
export class HeaderPageComponent implements OnInit {

  @Input() title : string;

  constructor(public userService : UserService) { }

  ngOnInit() {

  }

  changeToogle(){

    document.body.classList.toggle('dark', this.userService.toogleValue);
        if(this.userService.toogleValue)
        {
          this.userService.setModoDiurno();
        }else{

          this.userService.setModoNocturno();
        }
  }

  

}
