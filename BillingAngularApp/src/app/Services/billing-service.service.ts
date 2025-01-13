import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BillingsService {

  private apiUrl = 'https://localhost:7252/api/Billings';
                    

  constructor(private http: HttpClient) { }

  fetchBillings(): Observable<any[]> {
      return this.http.get<any[]>('/api/Billings'); 
  }
}
