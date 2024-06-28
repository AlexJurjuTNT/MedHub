/**
 * MedHub-Backend.Api
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

import {CreateLaboratoryRequest} from '../model/createLaboratoryRequest';
import {GroupingInfo} from '../model/groupingInfo';
import {LaboratoryDto} from '../model/laboratoryDto';
import {LoadResult} from '../model/loadResult';
import {SortingInfo} from '../model/sortingInfo';
import {SummaryInfo} from '../model/summaryInfo';
import {UpdateLaboratoryRequest} from '../model/updateLaboratoryRequest';

import {BASE_PATH} from '../variables';
import {Configuration} from '../configuration';


@Injectable()
export class LaboratoryService {

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
  public createLaboratory(body?: CreateLaboratoryRequest, observe?: 'body', reportProgress?: boolean): Observable<LaboratoryDto>;

  public createLaboratory(body?: CreateLaboratoryRequest, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<LaboratoryDto>>;

  public createLaboratory(body?: CreateLaboratoryRequest, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<LaboratoryDto>>;

  public createLaboratory(body?: CreateLaboratoryRequest, observe: any = 'body', reportProgress: boolean = false): Observable<any> {


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

    return this.httpClient.request<LaboratoryDto>('post', `${this.basePath}/api/v1/Laboratory`,
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
   * @param laboratoryId
   * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
   * @param reportProgress flag to report request and response progress.
   */
  public deleteLaboratory(laboratoryId: number, observe?: 'body', reportProgress?: boolean): Observable<any>;

  public deleteLaboratory(laboratoryId: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;

  public deleteLaboratory(laboratoryId: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;

  public deleteLaboratory(laboratoryId: number, observe: any = 'body', reportProgress: boolean = false): Observable<any> {

    if (laboratoryId === null || laboratoryId === undefined) {
      throw new Error('Required parameter laboratoryId was null or undefined when calling deleteLaboratory.');
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

    return this.httpClient.request<any>('delete', `${this.basePath}/api/v1/Laboratory/${encodeURIComponent(String(laboratoryId))}`,
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
  public getAllLaboratories(requireTotalCount?: boolean, requireGroupCount?: boolean, isCountQuery?: boolean, isSummaryQuery?: boolean, skip?: number, take?: number, sort?: Array<SortingInfo>, group?: Array<GroupingInfo>, filter?: Array<any>, totalSummary?: Array<SummaryInfo>, groupSummary?: Array<SummaryInfo>, select?: Array<string>, preSelect?: Array<string>, remoteSelect?: boolean, remoteGrouping?: boolean, expandLinqSumType?: boolean, primaryKey?: Array<string>, defaultSort?: string, stringToLower?: boolean, paginateViaPrimaryKey?: boolean, sortByPrimaryKey?: boolean, allowAsyncOverSync?: boolean, observe?: 'body', reportProgress?: boolean): Observable<LoadResult>;

  public getAllLaboratories(requireTotalCount?: boolean, requireGroupCount?: boolean, isCountQuery?: boolean, isSummaryQuery?: boolean, skip?: number, take?: number, sort?: Array<SortingInfo>, group?: Array<GroupingInfo>, filter?: Array<any>, totalSummary?: Array<SummaryInfo>, groupSummary?: Array<SummaryInfo>, select?: Array<string>, preSelect?: Array<string>, remoteSelect?: boolean, remoteGrouping?: boolean, expandLinqSumType?: boolean, primaryKey?: Array<string>, defaultSort?: string, stringToLower?: boolean, paginateViaPrimaryKey?: boolean, sortByPrimaryKey?: boolean, allowAsyncOverSync?: boolean, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<LoadResult>>;

  public getAllLaboratories(requireTotalCount?: boolean, requireGroupCount?: boolean, isCountQuery?: boolean, isSummaryQuery?: boolean, skip?: number, take?: number, sort?: Array<SortingInfo>, group?: Array<GroupingInfo>, filter?: Array<any>, totalSummary?: Array<SummaryInfo>, groupSummary?: Array<SummaryInfo>, select?: Array<string>, preSelect?: Array<string>, remoteSelect?: boolean, remoteGrouping?: boolean, expandLinqSumType?: boolean, primaryKey?: Array<string>, defaultSort?: string, stringToLower?: boolean, paginateViaPrimaryKey?: boolean, sortByPrimaryKey?: boolean, allowAsyncOverSync?: boolean, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<LoadResult>>;

  public getAllLaboratories(requireTotalCount?: boolean, requireGroupCount?: boolean, isCountQuery?: boolean, isSummaryQuery?: boolean, skip?: number, take?: number, sort?: Array<SortingInfo>, group?: Array<GroupingInfo>, filter?: Array<any>, totalSummary?: Array<SummaryInfo>, groupSummary?: Array<SummaryInfo>, select?: Array<string>, preSelect?: Array<string>, remoteSelect?: boolean, remoteGrouping?: boolean, expandLinqSumType?: boolean, primaryKey?: Array<string>, defaultSort?: string, stringToLower?: boolean, paginateViaPrimaryKey?: boolean, sortByPrimaryKey?: boolean, allowAsyncOverSync?: boolean, observe: any = 'body', reportProgress: boolean = false): Observable<any> {


    let queryParameters = new HttpParams({encoder: new CustomHttpUrlEncodingCodec()});
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

    return this.httpClient.request<LoadResult>('get', `${this.basePath}/api/v1/Laboratory`,
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
   * @param laboratoryId
   * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
   * @param reportProgress flag to report request and response progress.
   */
  public getLaboratoryById(laboratoryId: number, observe?: 'body', reportProgress?: boolean): Observable<LaboratoryDto>;

  public getLaboratoryById(laboratoryId: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<LaboratoryDto>>;

  public getLaboratoryById(laboratoryId: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<LaboratoryDto>>;

  public getLaboratoryById(laboratoryId: number, observe: any = 'body', reportProgress: boolean = false): Observable<any> {

    if (laboratoryId === null || laboratoryId === undefined) {
      throw new Error('Required parameter laboratoryId was null or undefined when calling getLaboratoryById.');
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

    return this.httpClient.request<LaboratoryDto>('get', `${this.basePath}/api/v1/Laboratory/${encodeURIComponent(String(laboratoryId))}`,
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
   * @param laboratoryId
   * @param body
   * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
   * @param reportProgress flag to report request and response progress.
   */
  public updateLaboratory(laboratoryId: number, body?: UpdateLaboratoryRequest, observe?: 'body', reportProgress?: boolean): Observable<LaboratoryDto>;

  public updateLaboratory(laboratoryId: number, body?: UpdateLaboratoryRequest, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<LaboratoryDto>>;

  public updateLaboratory(laboratoryId: number, body?: UpdateLaboratoryRequest, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<LaboratoryDto>>;

  public updateLaboratory(laboratoryId: number, body?: UpdateLaboratoryRequest, observe: any = 'body', reportProgress: boolean = false): Observable<any> {

    if (laboratoryId === null || laboratoryId === undefined) {
      throw new Error('Required parameter laboratoryId was null or undefined when calling updateLaboratory.');
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

    return this.httpClient.request<LaboratoryDto>('put', `${this.basePath}/api/v1/Laboratory/${encodeURIComponent(String(laboratoryId))}`,
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
