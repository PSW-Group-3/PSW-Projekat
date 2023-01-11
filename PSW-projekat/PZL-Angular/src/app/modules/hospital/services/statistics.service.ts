import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ExaminationsStatisticsDTO } from '../model/examinationStatisticDTO';
import { StatisticsDto } from '../model/statisticsdto.model';

@Injectable({
  providedIn: 'root'
})
export class StatisticsService {

  apiHost: string = 'http://localhost:16177/';
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  getStatistics(): Observable<StatisticsDto> {
    return this.http.get<StatisticsDto>('api/statistics', {headers: this.headers});
  }

  GetAllExaminationEventStatistics(): Observable<ExaminationsStatisticsDTO> {
    return this.http.get<ExaminationsStatisticsDTO>('api/ExaminationStatistic/GetAllExaminationEventStatistics', {headers: this.headers});
  }

}
