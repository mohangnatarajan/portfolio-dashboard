import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class ApiService {
  private http = inject(HttpClient);
  readonly baseUrl = environment.apiBaseUrl;

  getHello(): Observable<{ message: string }> {
    console.log(222,this.http.get<{ message: string }>(`${this.baseUrl}/api/hello`));
    return this.http.get<{ message: string }>(`${this.baseUrl}/api/hello`);
  }
}
