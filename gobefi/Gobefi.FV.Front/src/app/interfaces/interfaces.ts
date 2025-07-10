interface Modelo {
  id: number;
  marca: string;
  modelo: string;
  traccion: string;
  transmision: string;
  combustible: string;
  propulsion: string;
  cilindrada: string;
  carroceria: string;
  codigo_informe_tecnico: string;
  fecha_homologacion: string;
  categoria_vehiculo: string;
  empresa_homologacion: string;
  norma_emisiones: string;
  co2: string;
  rendimiento_ciudad: string;
  rendimiento_carretera: string;
  rendimiento_mixto: string;
  rendimiento_puro_electrico: string;
  rendimiento_enchufable_combustible: string;
  rendimiento_enchufable_electrico: string;
  tipo_de_conector_ac?: any;
  tipo_de_conector_dc?: any;
  acumulacion_energia_bateria: string;
  capacidad_convertidor_vehiculo_electrico: string;
  autonomia?: any;
  rendimiento?: any;
  created_at: string;
  updated_at: string;
  img: string;
  link?: any;
  eliminar: number;
}

interface ModeloSearch {
  id: number;
  idEm: number;
  marca: string;
  modelo: string;
}

interface BuscarModelo{
  marca? : string,
  propulsion?: string,
  carroceria?: string
}
interface Imagen{
  url? : string
}

interface TipoPropiedad {
  nombre: string;
  id: number;
  createdAt: string;
  updatedAt: string;
  version: number;
  active: boolean;
  modifiedBy?: any;
  createdBy?: any;
}

interface Institucion {
  nombre: string;
  id: number;
  createdAt: string;
  updatedAt: string;
  version: number;
  active: boolean;
  modifiedBy?: any;
  createdBy?: string;
}

interface Servicio {
  nombre: string;
  identificador: string;
  institucionId: number;
  reportaPMG: boolean;
  compraActiva: boolean;
  institucion?: any;
  id: number;
  createdAt: string;
  updatedAt: string;
  version: number;
  active: boolean;
  modifiedBy?: any;
  createdBy?: any;
}
interface Vehiculo {
  id?: number;
  patente?: string;
  nombre?: string;
  anio?: number;
  modeloId?: number;
  tipoPropiedadId?: number;
  tipoVehiculo?: string;
  marca?: string;
  modelootro? : string;
  traccion?: string;
  transmision?: string;
  propulsionId?: number;
  combustibleId?:number;
  cilindrada?: string;
  carroceria? : string;
  propulsion?: string;
  ministerioId?: number;
  servicioId?: number;
  regionId?:number;
  comunaId?:number;
  kilometraje?: number;
  modelo?: string;
  kilometrosPorLitro?: string;
  imagenes?: Imagen[];
}
interface filtro{
  tipo?: string,
  marca?: string,
  propulsion?: string,
  ministerio?:string
}

interface Imagen {
  id?: number;
  url?: string;
}

interface Lista{
  id?:number,
  nombre?:string
}

interface patch{
  op?: string,
  path?:string,
  value? : any
}

interface Usuario{
  id?: string,
  email? : string,
  role? : string,
  nombre?: string,
  sexo?:string
}

interface Menu {
  id?: number;
  nombre?: string;
  url?: string;
  icono?: string;
  orden?: number;
  permisos?: any[];
}

interface Permiso {
  id?: number;
  menuId?: number;
  role?: string;
  lectura?: boolean;
  escritura?: boolean;
  menu: Menu;
}

interface Region {
  id: number;
  nombre: string;
}

interface Comuna {
  id: number;
  nombre: string;
}

interface VehiculoOtro{
    patente:string;
    nombre : string;
    anio: number;
    tipoPropiedadI: number;
    ministerioId:number;
    servicioId:number;
    kilometraje:number;
    regionId : number;
    comunaId : number;
    marca : string;
    ModeloOtro : string;
    Traccion : string;
    Transmision : string;
    PropulsionId : number;
    CombustibleId : number;
    Cilindrada : string;
    Carroceria : string;
}

interface Propulsion {
  id: number;
  nombre: string;
}

