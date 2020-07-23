import { Injectable } from '@angular/core';

import { HttpClient, HttpHeaders } from "@angular/common/http";

import { Observable, of } from "rxjs";
import { catchError, map, tap } from "rxjs/operators";

import { Rate } from "./rate";

@Injectable({
  providedIn: 'root'
})
export class RatesService {

  private ratesUrl = '/api/rates';
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  getRatesByItem(id: number) : Observable<Rate[]> {
    return this.http.get<Rate[]>(`${ this.ratesUrl }/item/${ id }`)
    .pipe(
      tap(_ => console.log(`Fetched Rate by Item, ID: ${ id }`)),
      catchError(this.handleError<Rate[]>(`getRatesByItem, id=${ id }`))
    );
  }

  addRate(newRate: Rate): Observable<Rate> {
    return this.http.post<Rate>(this.ratesUrl, newRate, this.httpOptions)
      .pipe(
        tap((newRate: Rate) => console.log(`Add Rate w/ ID=${ newRate.rateId }`)),
        catchError(this.handleError<Rate>('addRate'))
      );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error)
      console.log(`${ operation } failed: ${ error.message }`);
      return of(result as T);
    };
  }
}
