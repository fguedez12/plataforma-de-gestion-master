import { Component, OnInit } from '@angular/core';
import { Platform } from '@ionic/angular';

@Component({
  selector: 'app-footer-page',
  templateUrl: './footer-page.component.html',
  styleUrls: ['./footer-page.component.scss'],
})
export class FooterPageComponent implements OnInit {

  pltWeb = false;
  constructor(private ptl : Platform,) { }

  ngOnInit() {

    if(!this.ptl.is('hybrid')){
      this.pltWeb = true;
    } 
  }

}
