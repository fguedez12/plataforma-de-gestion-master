import { Component, Input, OnInit, Output, EventEmitter, ViewChild, ElementRef } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ModalController, Platform, AlertController, PopoverController } from '@ionic/angular';
import { SectorPublicoService } from 'src/app/services/sector-publico.service';
import { VehiculosService } from '../../services/vehiculos.service';
import {CameraSource,Plugins,CameraResultType} from '@capacitor/core';
import { PopoverModelosComponent } from '../popover-modelos/popover-modelos.component';
import { UserService } from 'src/app/services/user.service';
import { PropulsionesService } from 'src/app/services/propulsiones.service';
import { CombustiblesService } from '../../services/combustibles.service';
const {Camera} = Plugins;

@Component({
  selector: 'app-modal-vehiculo',
  templateUrl: './modal-vehiculo.component.html',
  styleUrls: ['./modal-vehiculo.component.scss'],
})
export class ModalVehiculoComponent implements OnInit {

  @Input() vehiculo:Vehiculo;
  @Input() titulo:String;
  @Output() submitVeiculo= new EventEmitter<any>();
  @Output() editVeiculo= new EventEmitter<any>();
  @ViewChild('fileInput', {static:false}) fileInput : ElementRef;

  pltWeb = false;
  submitted = false;
  marcas : any[]=[];
  carrocerias: any[]=[];
  propulsiones:any[]=[];
  combustibles:any[]=[];
  imagenes : Imagen[]=[];
  maxyear : number = 2020 ;
  btnText = "Guardar";
  editMode = false;
  buscarModelo : BuscarModelo = {};
  modelos : Modelo[]=[];
  tiposPropiedad: TipoPropiedad[]=[];
  instituciones:Institucion[]=[];
  servicios:Servicio[]=[];
  regiones:Region[]=[];
  comunas:Comuna[] = [];
  isAdmin:boolean = false;
  swapForm: boolean = false;


  vehiculoForm  = this.formBuilder.group({
    nombre : [''],
    patente: ['', Validators.compose([Validators.required,Validators.minLength(6),Validators.minLength(6)])],
    modelo: ['',Validators.required],
    marca:[''],
    modelootro:[''],
    traccion:[''],
    transmision:[''],
    propulsionId:[''],
    combustibleId : [''],
    cilindrada : [''],
    carroceria : [''],
    modeloId : [''],
    anio :['',Validators.required],
    kilometraje:['',Validators.compose(
      [
        Validators.required,
        Validators.min(0),
        Validators.pattern(/^[0-9]+([.][0-9]+)?$/)
      ])],
    tipoPropiedadId: ['', Validators.required],
    ministerioId: ['', Validators.required],
    servicioId: [{value:'',disabled:true}, Validators.required],
    regionId: ['', Validators.required],
    comunaId: [{value:'',disabled:true}, Validators.required],
    imagenes:['']
  });

  constructor(
    private modalController: ModalController, 
    public formBuilder: FormBuilder,
    private vehiculosService : VehiculosService,
    private sectorPublicoService: SectorPublicoService,
    private ptl : Platform,
    private alertController : AlertController,
    private popoverController : PopoverController,
    private userService : UserService,
    private propulsionesService : PropulsionesService,
    private combustiblesService : CombustiblesService
    ) { }

  ngOnInit() {
    //console.log(this.vehiculo);
    if(!this.ptl.is('hybrid')){
      this.pltWeb = true;
    } 
    this.isAdmin = this.userService.isAdmin;
    this.loadData();
    
    setTimeout(() => {
      this.setmaxYear();
      this.setVehiculoData();
    }, 500);
  }


  loadData(){
    this.sectorPublicoService.getTiposPropiedad()
    .subscribe(resp=>{
      this.tiposPropiedad.push(...resp);
    });

    this.sectorPublicoService.getRegiones()
    .subscribe(resp=>{
      this.regiones.push(...resp);
      this.vehiculoForm.patchValue({
        'regionId': this.vehiculo.regionId,
        });
    });
    this.propulsionesService.getPropulsiones()
    .subscribe(resp=>{
      console.log(resp)
      this.propulsiones.push(...resp);
    });
    this.combustiblesService.getCombustibles()
    .subscribe(resp=>{
      console.log(resp)
      this.combustibles.push(...resp);
    });


    if(this.isAdmin){
      this.sectorPublicoService.getInstituciones()
      .subscribe(resp=>{
      
        this.instituciones.push(...resp);
        this.vehiculoForm.patchValue({
          'ministerioId': this.vehiculo.ministerioId,
          });
      });
    }else{
      this.sectorPublicoService.getInstitucionesById()
      .subscribe(resp=>{
      
        this.instituciones.push(...resp);
        this.vehiculoForm.patchValue({
          'ministerioId': this.vehiculo.ministerioId,
          });
      });
    }


  }

  setmaxYear(){
    var today = new Date();
    var year = today.getFullYear();
    this.maxyear = year+1;
    this.vehiculoForm.controls["anio"].setValidators(Validators.compose([Validators.required,Validators.min(1900),Validators.max(this.maxyear)]));
  }

  setVehiculoData(){
    if(this.vehiculo.patente){

      if(this.vehiculo.modeloId==null)
      {
        this.setValidationOtro();
        this.swapForm = !this.swapForm;
        this.vehiculoForm.patchValue({
          'nombre': this.vehiculo.nombre,
          'marca':this.vehiculo.marca,
          'modelootro':this.vehiculo.modelootro,
          'traccion' : this.vehiculo.traccion,
          'transmision' : this.vehiculo.transmision,
          'propulsionId' : this.vehiculo.propulsionId,
          'combustibleId' : this.vehiculo.combustibleId,
          'cilindrada' : this.vehiculo.cilindrada,
          'carroceria'  :this.vehiculo.carroceria,
          'patente': this.vehiculo.patente,
          'anio': this.vehiculo.anio,
          'kilometraje': this.vehiculo.kilometraje,
          'tipoPropiedadId': this.vehiculo.tipoPropiedadId,
          'modeloId': this.vehiculo.modeloId  
          });
      }else{
        this.setValidationAuto();
        this.vehiculoForm.patchValue({
          'nombre': this.vehiculo.nombre,
          'patente': this.vehiculo.patente,
          'anio': this.vehiculo.anio,
          'kilometraje': this.vehiculo.kilometraje,
          'tipoPropiedadId': this.vehiculo.tipoPropiedadId,
          'modelo' : `${this.vehiculo.marca}, ${this.vehiculo.modelo}`,
          'modeloId': this.vehiculo.modeloId  
          });
      }
      this.imagenes = this.vehiculo.imagenes;
          this.btnText = "Guardar cambios"
          this.editMode = true;
          return ;
    }

    this.editMode = false;
    
  }

  get errorCtr(){
    return this.vehiculoForm.controls;
  }

  closeModal(){
    this.modalController.dismiss();

  }

  submitForm(){
    this.submitted=true;
    console.log(this.vehiculoForm.valid);
    if(this.vehiculoForm.valid)
    {
     this.vehiculoForm.patchValue({
      imagenes : this.imagenes,
     });
      if(this.editMode)
      {
        //console.log("Edit Mode")
        this.editVeiculo.emit(this.vehiculoForm.value);
      }else{
        //console.log("Save Mode")
        this.submitVeiculo.emit(this.vehiculoForm.value);
      }
      this.modalController.dismiss();
    }
  }

  changeInstitucion(id)
  {
    
    if(id!='' && id){
      this.sectorPublicoService.getServicios(id)
      .subscribe(resp=>{
        this.servicios = [];
        this.servicios.push(...resp);
        this.vehiculoForm.controls['servicioId'].setValue('');
        this.vehiculoForm.controls['servicioId'].enable();
        this.vehiculoForm.patchValue({
          'servicioId': this.vehiculo.servicioId,
      });
      });
    }
  }

  changeRegion(id)
  {
    
    if(id!='' && id){
      this.sectorPublicoService.getComunasByRegionId(id)
      .subscribe(resp=>{
        this.comunas = [];
        this.comunas.push(...resp);
        this.vehiculoForm.controls['comunaId'].setValue('');
        this.vehiculoForm.controls['comunaId'].enable();
        this.vehiculoForm.patchValue({
          'comunaId': this.vehiculo.comunaId,
      });
      });
    }
  }

  camaraImage()  {
    this.addImage(CameraSource.Camera);
  }

  galeriaImage(){
    this.addImage(CameraSource.Photos);
  }

  uploadFile(event : EventTarget){
    const eventObj: MSInputMethodContext  = event as MSInputMethodContext;
    const target :  HTMLInputElement = eventObj.target as HTMLInputElement;
    const file : File  = target.files[0];
    
    const formData = new FormData();
    formData.append('file',file,file.name);
    this.vehiculosService.postImage(formData)
    .subscribe(resp=>{
      
      this.imagenes.push({id:0, url :resp.url } );
    })
    
  }

  uploadImage(blobData, ext){
    const formData = new FormData();
    formData.append('file',blobData,`myImage.${ext}`);
    this.vehiculosService.postImage(formData)
    .subscribe(resp=>{
      
      this.imagenes.push({id:0, url :resp.url } );
    })
  }

  fileImage(){
    this.fileInput.nativeElement.click();
  }

  async addImage(source : CameraSource){

    const image = await Camera.getPhoto({
      quality: 60,
      allowEditing: false,
      resultType: CameraResultType.Base64,
      source
    });

    
    const blobData = this.base64toBlob(image.base64String,`image/${image.format}`);
    this.uploadImage(blobData,image.format);

  }

  base64toBlob(base64Data, contentType) {
    contentType = contentType || '';
    var sliceSize = 1024;
    var byteCharacters = atob(base64Data);
    var bytesLength = byteCharacters.length;
    var slicesCount = Math.ceil(bytesLength / sliceSize);
    var byteArrays = new Array(slicesCount);

    for (var sliceIndex = 0; sliceIndex < slicesCount; ++sliceIndex) {
        var begin = sliceIndex * sliceSize;
        var end = Math.min(begin + sliceSize, bytesLength);

        var bytes = new Array(end - begin);
        for (var offset = begin, i = 0; offset < end; ++i, ++offset) {
            bytes[i] = byteCharacters[offset].charCodeAt(0);
        }
        byteArrays[sliceIndex] = new Uint8Array(bytes);
    }
    return new Blob(byteArrays, { type: contentType });
}

  deleteImage(index : number, id : string, url: string){
    if(id!="0"){
     
      this.vehiculosService.deleteImage(id,url).subscribe(resp=>{
       
      });
    }
    this.imagenes.splice(index,1);

  }

  processCarrocerias(lista : string[]){

    var carroceria = this.processData(lista,this.carrocerias,this.vehiculo.tipoVehiculo)
    if(carroceria){
      this.vehiculoForm.patchValue({
        'tipoVehiculo': carroceria.id
      });
    }
   
   
  }

  processMarcas(lista : string[]){
    
    var marcaObj = this.processData(lista,this.marcas,this.vehiculo.marca)
    if(marcaObj){
      this.vehiculoForm.patchValue({
        'marca': marcaObj.id
      });
    }
  }

  processPropulsion(lista : string[]){
   
    var propObj = this.processData(lista,this.propulsiones,this.vehiculo.propulsion)
    
    if(propObj){
      this.vehiculoForm.patchValue({
        'propulsion': propObj.id
      });
    }
  }

  processData(lista : string[], arreglo : any[], itemStr : string){
    let objList = [];
    var i;
    for (i = 0; i < lista.length; i++) {
      var obj = { id : i+1, nombre : lista[i] };
      objList.push(obj);
    }
    arreglo.push(...objList);
    
   
    if(itemStr){
      var itemObj = arreglo.find(function(element) {
        return element.nombre == itemStr;
      });
      return itemObj;
    }
   
  }

  numberOnlyValidation(event: any) {
    const pattern = /[0-9]/;
    let inputChar = String.fromCharCode(event.charCode);

    if (!pattern.test(inputChar)) {
      // invalid character, prevent input
      event.preventDefault();
    }
  }

  async presentAlert() {
    const alert = await this.alertController.create({
      cssClass: 'my-custom-class',
      header: 'Aviso',
      subHeader: '',
      message: 'No existen marcas según los criterios de busqueda',
      buttons: ['OK']
    });

    await alert.present();
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
      this.vehiculoForm.patchValue({
        'modelo': `${data.marca}, ${data.modelo}`,
        'modeloId': data.id
      });
    }
    
    //console.log('vehiculo', data);
  }

  toogleForm(){
    this.swapForm = !this.swapForm;
    if(this.swapForm)
    {
     this.setValidationOtro();
    }else{
      this.setValidationAuto();
      
    }
  }

  setValidationAuto()
  {
    this.vehiculoForm.get('modelo').setValidators([Validators.required]);
    this.vehiculoForm.get('modelo').updateValueAndValidity();
    this.vehiculoForm.get('marca').clearValidators();
    this.vehiculoForm.get('marca').updateValueAndValidity();
    this.vehiculoForm.get('modelootro').clearValidators();
    this.vehiculoForm.get('modelootro').updateValueAndValidity();
    this.vehiculoForm.get('traccion').clearValidators();
    this.vehiculoForm.get('traccion').updateValueAndValidity();
    this.vehiculoForm.get('transmision').clearValidators();
    this.vehiculoForm.get('transmision').updateValueAndValidity();
    this.vehiculoForm.get('propulsionId').clearValidators();
    this.vehiculoForm.get('propulsionId').updateValueAndValidity();
    this.vehiculoForm.get('combustibleId').clearValidators();
    this.vehiculoForm.get('combustibleId').updateValueAndValidity();
    this.vehiculoForm.get('cilindrada').clearValidators();
    this.vehiculoForm.get('cilindrada').updateValueAndValidity();
    this.vehiculoForm.get('carroceria').clearValidators();
    this.vehiculoForm.get('carroceria').updateValueAndValidity();
  }

  setValidationOtro()
  {
    this.vehiculoForm.get('modelo').clearValidators();
    this.vehiculoForm.get('modelo').updateValueAndValidity();
    this.vehiculoForm.get('marca').setValidators([Validators.required]);
    this.vehiculoForm.get('marca').updateValueAndValidity();
    this.vehiculoForm.get('modelootro').setValidators([Validators.required]);
    this.vehiculoForm.get('modelootro').updateValueAndValidity();
    this.vehiculoForm.get('traccion').setValidators([Validators.required]);
    this.vehiculoForm.get('traccion').updateValueAndValidity();
    this.vehiculoForm.get('transmision').setValidators([Validators.required]);
    this.vehiculoForm.get('transmision').updateValueAndValidity();
    this.vehiculoForm.get('propulsionId').setValidators([Validators.required]);
    this.vehiculoForm.get('propulsionId').updateValueAndValidity();
    this.vehiculoForm.get('combustibleId').setValidators([Validators.required]);
    this.vehiculoForm.get('combustibleId').updateValueAndValidity();
    this.vehiculoForm.get('cilindrada').setValidators([Validators.required]);
    this.vehiculoForm.get('cilindrada').updateValueAndValidity();
    this.vehiculoForm.get('carroceria').setValidators([Validators.required]);
    this.vehiculoForm.get('carroceria').updateValueAndValidity();
  }

  
}
