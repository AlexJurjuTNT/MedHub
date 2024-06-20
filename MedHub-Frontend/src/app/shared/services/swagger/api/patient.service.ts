/**
 * MedHub-Backend
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.0
 *
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 *//* tslint:disable:no-unused-variable member-ordering */

import {Inject, Injectable, Optional} from '@angular/core';
import {HttpClient, HttpEvent, HttpHeaders, HttpParams, HttpResponse} from '@angular/common/http';
import {CustomHttpUrlEncodingCodec} from '../encoder';

import {Observable} from 'rxjs';

import {AddPatientDataDto} from '../model/addPatientDataDto';
import {GroupingInfo} from '../model/groupingInfo';
import {LoadResult} from '../model/loadResult';
import {PatientDto} from '../model/patientDto';
import {SortingInfo} from '../model/sortingInfo';
import {SummaryInfo} from '../model/summaryInfo';
import {TestRequestDto} from '../model/testRequestDto';
import {UpdatePatientDto} from '../model/updatePatientDto';
import {UserDto} from '../model/userDto';

import {BASE_PATH} from '../variables';
import {Configuration} from '../configuration';


@Injectable()
export class PatientService {

  public defaultHeaders = new HttpHeaders();
  public configuration = new Configuration();
  protected basePath = 'http://localhost:5210';

  constructor(protected httpClient: HttpClient, @Optional() @Inject(BASE_PATH) basePath: string, @Optional() configuration: Configuration) {
    if (basePath) {
      this.basePath = basePath;
    }
    if (configuration) {
      this.configuration = configuration;
      this.basePath = basePath || configuration.basePath || this.basePath;
    }
  }

  /**
   *
   *
   * @param body
   * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
   * @param reportProgress flag to report request and response progress.
   */
  public addPatientInformation(body?: AddPatientDataDto, observe?: 'body', reportProgress?: boolean): Observable<PatientDto>;

  public addPatientInformation(body?: AddPatientDataDto, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<PatientDto>>;

  public addPatientInformation(body?: AddPatientDataDto, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<PatientDto>>;

  public addPatientInformation(body?: AddPatientDataDto, observe: any = 'body', reportProgress: boolean = false): Observable<any> {


    let headers = this.defaultHeaders;

    // authentication (Bearer) required
    if (this.configuration.accessToken) {
      const accessToken = typeof this.configuration.accessToken === 'function'
        ? this.configuration.accessToken()
        : this.configuration.accessToken;
      headers = headers.set('Authorization', 'Bearer ' + accessToken);
    }
    // to determine the Accept header
    let httpHeaderAccepts: string[] = [
      'text/plain',
      'application/json',
      'text/json'
    ];
    const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
    if (httpHeaderAcceptSelected != undefined) {
      headers = headers.set('Accept', httpHeaderAcceptSelected);
    }

    // to determine the Content-Type header
    const consumes: string[] = [
      'application/json',
      'text/json',
      'application/_*+json'
    ];
    const httpContentTypeSelected: string | undefined = this.configuration.selectHeaderContentType(consumes);
    if (httpContentTypeSelected != undefined) {
      headers = headers.set('Content-Type', httpContentTypeSelected);
    }

    return this.httpClient.request<PatientDto>('post', `${this.basePath}/api/v1/Patient`,
      {
        body: body,
        withCredentials: this.configuration.withCredentials,
        headers: headers,
        observe: observe,
        reportProgress: reportProgress
      }
    );
  }

  /**
   *
   *
   * @param patientId
   * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
   * @param reportProgress flag to report request and response progress.
   */
  public deletePatientInformation(patientId: number, observe?: 'body', reportProgress?: boolean): Observable<any>;

  public deletePatientInformation(patientId: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;

  public deletePatientInformation(patientId: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;

  public deletePatientInformation(patientId: number, observe: any = 'body', reportProgress: boolean = false): Observable<any> {

    if (patientId === null || patientId === undefined) {
      throw new Error('Required parameter patientId was null or undefined when calling deletePatientInformation.');
    }

    let headers = this.defaultHeaders;

    // authentication (Bearer) required
    if (this.configuration.accessToken) {
      const accessToken = typeof this.configuration.accessToken === 'function'
        ? this.configuration.accessToken()
        : this.configuration.accessToken;
      headers = headers.set('Authorization', 'Bearer ' + accessToken);
    }
    // to determine the Accept header
    let httpHeaderAccepts: string[] = [];
    const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
    if (httpHeaderAcceptSelected != undefined) {
      headers = headers.set('Accept', httpHeaderAcceptSelected);
    }

    // to determine the Content-Type header
    const consumes: string[] = [];

    return this.httpClient.request<any>('delete', `${this.basePath}/api/v1/Patient/${encodeURIComponent(String(patientId))}`,
      {
        withCredentials: this.configuration.withCredentials,
        headers: headers,
        observe: observe,
        reportProgress: reportProgress
      }
    );
  }

  /**
   *
   *
   * @param clinicId
   * @param requireTotalCount
   * @param requireGroupCount
   * @param isCountQuery
   * @param isSummaryQuery
   * @param skip
   * @param take
   * @param sort
   * @param group
   * @param filter
   * @param totalSummary
   * @param groupSummary
   * @param select
   * @param preSelect
   * @param remoteSelect
   * @param remoteGrouping
   * @param expandLinqSumType
   * @param primaryKey
   * @param defaultSort
   * @param stringToLower
   * @param paginateViaPrimaryKey
   * @param sortByPrimaryKey
   * @param allowAsyncOverSync
   * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
   * @param reportProgress flag to report request and response progress.
   */
  public getAllPatientsOfClinic(clinicId?: number, requireTotalCount?: boolean, requireGroupCount?: boolean, isCountQuery?: boolean, isSummaryQuery?: boolean, skip?: number, take?: number, sort?: Array<SortingInfo>, group?: Array<GroupingInfo>, filter?: Array<any>, totalSummary?: Array<SummaryInfo>, groupSummary?: Array<SummaryInfo>, select?: Array<string>, preSelect?: Array<string>, remoteSelect?: boolean, remoteGrouping?: boolean, expandLinqSumType?: boolean, primaryKey?: Array<string>, defaultSort?: string, stringToLower?: boolean, paginateViaPrimaryKey?: boolean, sortByPrimaryKey?: boolean, allowAsyncOverSync?: boolean, observe?: 'body', reportProgress?: boolean): Observable<LoadResult>;

  public getAllPatientsOfClinic(clinicId?: number, requireTotalCount?: boolean, requireGroupCount?: boolean, isCountQuery?: boolean, isSummaryQuery?: boolean, skip?: number, take?: number, sort?: Array<SortingInfo>, group?: Array<GroupingInfo>, filter?: Array<any>, totalSummary?: Array<SummaryInfo>, groupSummary?: Array<SummaryInfo>, select?: Array<string>, preSelect?: Array<string>, remoteSelect?: boolean, remoteGrouping?: boolean, expandLinqSumType?: boolean, primaryKey?: Array<string>, defaultSort?: string, stringToLower?: boolean, paginateViaPrimaryKey?: boolean, sortByPrimaryKey?: boolean, allowAsyncOverSync?: boolean, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<LoadResult>>;

  public getAllPatientsOfClinic(clinicId?: number, requireTotalCount?: boolean, requireGroupCount?: boolean, isCountQuery?: boolean, isSummaryQuery?: boolean, skip?: number, take?: number, sort?: Array<SortingInfo>, group?: Array<GroupingInfo>, filter?: Array<any>, totalSummary?: Array<SummaryInfo>, groupSummary?: Array<SummaryInfo>, select?: Array<string>, preSelect?: Array<string>, remoteSelect?: boolean, remoteGrouping?: boolean, expandLinqSumType?: boolean, primaryKey?: Array<string>, defaultSort?: string, stringToLower?: boolean, paginateViaPrimaryKey?: boolean, sortByPrimaryKey?: boolean, allowAsyncOverSync?: boolean, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<LoadResult>>;

  public getAllPatientsOfClinic(clinicId?: number, requireTotalCount?: boolean, requireGroupCount?: boolean, isCountQuery?: boolean, isSummaryQuery?: boolean, skip?: number, take?: number, sort?: Array<SortingInfo>, group?: Array<GroupingInfo>, filter?: Array<any>, totalSummary?: Array<SummaryInfo>, groupSummary?: Array<SummaryInfo>, select?: Array<string>, preSelect?: Array<string>, remoteSelect?: boolean, remoteGrouping?: boolean, expandLinqSumType?: boolean, primaryKey?: Array<string>, defaultSort?: string, stringToLower?: boolean, paginateViaPrimaryKey?: boolean, sortByPrimaryKey?: boolean, allowAsyncOverSync?: boolean, observe: any = 'body', reportProgress: boolean = false): Observable<any> {


    let queryParameters = new HttpParams({encoder: new CustomHttpUrlEncodingCodec()});
    if (clinicId !== undefined && clinicId !== null) {
      queryParameters = queryParameters.set('clinicId', <any>clinicId);
    }
    if (requireTotalCount !== undefined && requireTotalCount !== null) {
      queryParameters = queryParameters.set('RequireTotalCount', <any>requireTotalCount);
    }
    if (requireGroupCount !== undefined && requireGroupCount !== null) {
      queryParameters = queryParameters.set('RequireGroupCount', <any>requireGroupCount);
    }
    if (isCountQuery !== undefined && isCountQuery !== null) {
      queryParameters = queryParameters.set('IsCountQuery', <any>isCountQuery);
    }
    if (isSummaryQuery !== undefined && isSummaryQuery !== null) {
      queryParameters = queryParameters.set('IsSummaryQuery', <any>isSummaryQuery);
    }
    if (skip !== undefined && skip !== null) {
      queryParameters = queryParameters.set('Skip', <any>skip);
    }
    if (take !== undefined && take !== null) {
      queryParameters = queryParameters.set('Take', <any>take);
    }
    if (sort) {
      sort.forEach((element) => {
        queryParameters = queryParameters.append('Sort', <any>element);
      })
    }
    if (group) {
      group.forEach((element) => {
        queryParameters = queryParameters.append('Group', <any>element);
      })
    }
    if (filter) {
      filter.forEach((element) => {
        queryParameters = queryParameters.append('Filter', <any>element);
      })
    }
    if (totalSummary) {
      totalSummary.forEach((element) => {
        queryParameters = queryParameters.append('TotalSummary', <any>element);
      })
    }
    if (groupSummary) {
      groupSummary.forEach((element) => {
        queryParameters = queryParameters.append('GroupSummary', <any>element);
      })
    }
    if (select) {
      select.forEach((element) => {
        queryParameters = queryParameters.append('Select', <any>element);
      })
    }
    if (preSelect) {
      preSelect.forEach((element) => {
        queryParameters = queryParameters.append('PreSelect', <any>element);
      })
    }
    if (remoteSelect !== undefined && remoteSelect !== null) {
      queryParameters = queryParameters.set('RemoteSelect', <any>remoteSelect);
    }
    if (remoteGrouping !== undefined && remoteGrouping !== null) {
      queryParameters = queryParameters.set('RemoteGrouping', <any>remoteGrouping);
    }
    if (expandLinqSumType !== undefined && expandLinqSumType !== null) {
      queryParameters = queryParameters.set('ExpandLinqSumType', <any>expandLinqSumType);
    }
    if (primaryKey) {
      primaryKey.forEach((element) => {
        queryParameters = queryParameters.append('PrimaryKey', <any>element);
      })
    }
    if (defaultSort !== undefined && defaultSort !== null) {
      queryParameters = queryParameters.set('DefaultSort', <any>defaultSort);
    }
    if (stringToLower !== undefined && stringToLower !== null) {
      queryParameters = queryParameters.set('StringToLower', <any>stringToLower);
    }
    if (paginateViaPrimaryKey !== undefined && paginateViaPrimaryKey !== null) {
      queryParameters = queryParameters.set('PaginateViaPrimaryKey', <any>paginateViaPrimaryKey);
    }
    if (sortByPrimaryKey !== undefined && sortByPrimaryKey !== null) {
      queryParameters = queryParameters.set('SortByPrimaryKey', <any>sortByPrimaryKey);
    }
    if (allowAsyncOverSync !== undefined && allowAsyncOverSync !== null) {
      queryParameters = queryParameters.set('AllowAsyncOverSync', <any>allowAsyncOverSync);
    }

    let headers = this.defaultHeaders;

    // authentication (Bearer) required
    if (this.configuration.accessToken) {
      const accessToken = typeof this.configuration.accessToken === 'function'
        ? this.configuration.accessToken()
        : this.configuration.accessToken;
      headers = headers.set('Authorization', 'Bearer ' + accessToken);
    }
    // to determine the Accept header
    let httpHeaderAccepts: string[] = [
      'text/plain',
      'application/json',
      'text/json'
    ];
    const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
    if (httpHeaderAcceptSelected != undefined) {
      headers = headers.set('Accept', httpHeaderAcceptSelected);
    }

    // to determine the Content-Type header
    const consumes: string[] = [];

    return this.httpClient.request<LoadResult>('get', `${this.basePath}/api/v1/Patient/paged`,
      {
        params: queryParameters,
        withCredentials: this.configuration.withCredentials,
        headers: headers,
        observe: observe,
        reportProgress: reportProgress
      }
    );
  }

  /**
   *
   *
   * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
   * @param reportProgress flag to report request and response progress.
   */
  public getAllUserPatients(observe?: 'body', reportProgress?: boolean): Observable<Array<UserDto>>;

  public getAllUserPatients(observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<UserDto>>>;

  public getAllUserPatients(observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<UserDto>>>;

  public getAllUserPatients(observe: any = 'body', reportProgress: boolean = false): Observable<any> {

    let headers = this.defaultHeaders;

    // authentication (Bearer) required
    if (this.configuration.accessToken) {
      const accessToken = typeof this.configuration.accessToken === 'function'
        ? this.configuration.accessToken()
        : this.configuration.accessToken;
      headers = headers.set('Authorization', 'Bearer ' + accessToken);
    }
    // to determine the Accept header
    let httpHeaderAccepts: string[] = [
      'text/plain',
      'application/json',
      'text/json'
    ];
    const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
    if (httpHeaderAcceptSelected != undefined) {
      headers = headers.set('Accept', httpHeaderAcceptSelected);
    }

    // to determine the Content-Type header
    const consumes: string[] = [];

    return this.httpClient.request<Array<UserDto>>('get', `${this.basePath}/api/v1/Patient/user-patients`,
      {
        withCredentials: this.configuration.withCredentials,
        headers: headers,
        observe: observe,
        reportProgress: reportProgress
      }
    );
  }

  /**
   *
   *
   * @param userId
   * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
   * @param reportProgress flag to report request and response progress.
   */
  public getPatientInformationForUser(userId: number, observe?: 'body', reportProgress?: boolean): Observable<PatientDto>;

  public getPatientInformationForUser(userId: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<PatientDto>>;

  public getPatientInformationForUser(userId: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<PatientDto>>;

  public getPatientInformationForUser(userId: number, observe: any = 'body', reportProgress: boolean = false): Observable<any> {

    if (userId === null || userId === undefined) {
      throw new Error('Required parameter userId was null or undefined when calling getPatientInformationForUser.');
    }

    let headers = this.defaultHeaders;

    // authentication (Bearer) required
    if (this.configuration.accessToken) {
      const accessToken = typeof this.configuration.accessToken === 'function'
        ? this.configuration.accessToken()
        : this.configuration.accessToken;
      headers = headers.set('Authorization', 'Bearer ' + accessToken);
    }
    // to determine the Accept header
    let httpHeaderAccepts: string[] = [
      'text/plain',
      'application/json',
      'text/json'
    ];
    const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
    if (httpHeaderAcceptSelected != undefined) {
      headers = headers.set('Accept', httpHeaderAcceptSelected);
    }

    // to determine the Content-Type header
    const consumes: string[] = [];

    return this.httpClient.request<PatientDto>('get', `${this.basePath}/api/v1/Patient/${encodeURIComponent(String(userId))}`,
      {
        withCredentials: this.configuration.withCredentials,
        headers: headers,
        observe: observe,
        reportProgress: reportProgress
      }
    );
  }

  /**
   *
   *
   * @param patientId
   * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
   * @param reportProgress flag to report request and response progress.
   */
  public getTestRequestsOfPatient(patientId: number, observe?: 'body', reportProgress?: boolean): Observable<Array<TestRequestDto>>;

  public getTestRequestsOfPatient(patientId: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<TestRequestDto>>>;

  public getTestRequestsOfPatient(patientId: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<TestRequestDto>>>;

  public getTestRequestsOfPatient(patientId: number, observe: any = 'body', reportProgress: boolean = false): Observable<any> {

    if (patientId === null || patientId === undefined) {
      throw new Error('Required parameter patientId was null or undefined when calling getTestRequestsOfPatient.');
    }

    let headers = this.defaultHeaders;

    // authentication (Bearer) required
    if (this.configuration.accessToken) {
      const accessToken = typeof this.configuration.accessToken === 'function'
        ? this.configuration.accessToken()
        : this.configuration.accessToken;
      headers = headers.set('Authorization', 'Bearer ' + accessToken);
    }
    // to determine the Accept header
    let httpHeaderAccepts: string[] = [
      'text/plain',
      'application/json',
      'text/json'
    ];
    const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
    if (httpHeaderAcceptSelected != undefined) {
      headers = headers.set('Accept', httpHeaderAcceptSelected);
    }

    // to determine the Content-Type header
    const consumes: string[] = [];

    return this.httpClient.request<Array<TestRequestDto>>('get', `${this.basePath}/api/v1/Patient/${encodeURIComponent(String(patientId))}/test-requests`,
      {
        withCredentials: this.configuration.withCredentials,
        headers: headers,
        observe: observe,
        reportProgress: reportProgress
      }
    );
  }

  /**
   *
   *
   * @param patientId
   * @param body
   * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
   * @param reportProgress flag to report request and response progress.
   */
  public updatePatientInformation(patientId: number, body?: UpdatePatientDto, observe?: 'body', reportProgress?: boolean): Observable<PatientDto>;

  public updatePatientInformation(patientId: number, body?: UpdatePatientDto, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<PatientDto>>;

  public updatePatientInformation(patientId: number, body?: UpdatePatientDto, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<PatientDto>>;

  public updatePatientInformation(patientId: number, body?: UpdatePatientDto, observe: any = 'body', reportProgress: boolean = false): Observable<any> {

    if (patientId === null || patientId === undefined) {
      throw new Error('Required parameter patientId was null or undefined when calling updatePatientInformation.');
    }


    let headers = this.defaultHeaders;

    // authentication (Bearer) required
    if (this.configuration.accessToken) {
      const accessToken = typeof this.configuration.accessToken === 'function'
        ? this.configuration.accessToken()
        : this.configuration.accessToken;
      headers = headers.set('Authorization', 'Bearer ' + accessToken);
    }
    // to determine the Accept header
    let httpHeaderAccepts: string[] = [
      'text/plain',
      'application/json',
      'text/json'
    ];
    const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
    if (httpHeaderAcceptSelected != undefined) {
      headers = headers.set('Accept', httpHeaderAcceptSelected);
    }

    // to determine the Content-Type header
    const consumes: string[] = [
      'application/json',
      'text/json',
      'application/_*+json'
    ];
    const httpContentTypeSelected: string | undefined = this.configuration.selectHeaderContentType(consumes);
    if (httpContentTypeSelected != undefined) {
      headers = headers.set('Content-Type', httpContentTypeSelected);
    }

    return this.httpClient.request<PatientDto>('put', `${this.basePath}/api/v1/Patient/${encodeURIComponent(String(patientId))}`,
      {
        body: body,
        withCredentials: this.configuration.withCredentials,
        headers: headers,
        observe: observe,
        reportProgress: reportProgress
      }
    );
  }

  /**
   * @param consumes string[] mime-types
   * @return true: consumes contains 'multipart/form-data', false: otherwise
   */
  private canConsumeForm(consumes: string[]): boolean {
    const form = 'multipart/form-data';
    for (const consume of consumes) {
      if (form === consume) {
        return true;
      }
    }
    return false;
  }

}
