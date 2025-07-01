import { Injectable } from '@angular/core';
import { Time } from '../models/time';

@Injectable({
  providedIn: 'root'
})
export class TimeMapperService {

  constructor() { }

  paraModelo(retornoAPI: any) : Time{
    let time: Time = {} as Time;
    Object.assign(time, retornoAPI)
    return time;
  }

  paraModelos(retornoAPI: any) : Time[]{
    let times: Time[] = [];
    Object.assign(times, retornoAPI);
    return times;
  }
}
