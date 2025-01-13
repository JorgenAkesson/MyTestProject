import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PatientService {

  private apiUrl = 'https://localhost:7120/api/Patients';

  constructor(private http: HttpClient) { }

  fetchPatients(): Observable<any[]> {
      return this.http.get<any[]>('/api/Patients'); 
  }

  createPatient(patient: any): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<any>(this.apiUrl, patient, { headers });
  }
}
