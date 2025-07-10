import { Component, OnInit } from '@angular/core';
import { ReporteService } from 'src/app/services/reporte.service';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-reportes',
  templateUrl: './reportes.page.html',
  styleUrls: ['./reportes.page.scss'],
})
export class ReportesPage implements OnInit {

 
  title : string = "Reportes" 
  btnText : string = "Descargar";

  constructor(private userService: UserService, private reporteService : ReporteService) { }

  ngOnInit() {
    this.userService.getUser();
  }


  getReporte()
  {
    this.btnText= "Cargando...";
    this.reporteService.getVehiculoReporte().subscribe(resp=>{
      //console.log(resp);
      const url = window.URL.createObjectURL(
      new Blob([resp]) );
      const link = window.document.createElement("a");
      link.href = url;
      link.setAttribute("download", "ReporteVehiculos.xlsx");
      window.document.body.appendChild(link);
      link.click();
      link.remove();  
      this.btnText= "Descargar";
    });
  }

}
