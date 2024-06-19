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
import {HttpClient, HttpEvent, HttpHeaders, HttpResponse} from '@angular/common/http';

import {Observable} from 'rxjs';
import {UserDto} from '../model/userDto';

import {BASE_PATH} from '../variables';
import {Configuration} from '../configuration';


@Injectable()
export class UserService {

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
   * Create a new user
   *
   * @param body User to be created
   * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
   * @param reportProgress flag to report request and response progress.
   */
  public createUser(body?: UserDto, observe?: 'body', reportProgress?: boolean): Observable<UserDto>;

  public createUser(body?: UserDto, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<UserDto>>;

  public createUser(body?: UserDto, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<UserDto>>;

  public createUser(body?: UserDto, observe: any = 'body', reportProgress: boolean = false): Observable<any> {


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

    return this.httpClient.request<UserDto>('post', `${this.basePath}/api/v1/User`,
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
   * Delete a user
   *
   * @param userId ID of the user to be deleted
   * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
   * @param reportProgress flag to report request and response progress.
   */
  public deleteUser(userId: number, observe?: 'body', reportProgress?: boolean): Observable<any>;

  public deleteUser(userId: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;

  public deleteUser(userId: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;

  public deleteUser(userId: number, observe: any = 'body', reportProgress: boolean = false): Observable<any> {

    if (userId === null || userId === undefined) {
      throw new Error('Required parameter userId was null or undefined when calling deleteUser.');
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

    return this.httpClient.request<any>('delete', `${this.basePath}/api/v1/User/${encodeURIComponent(String(userId))}`,
      {
        withCredentials: this.configuration.withCredentials,
        headers: headers,
        observe: observe,
        reportProgress: reportProgress
      }
    );
  }

  /**
   * Get all users
   *
   * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
   * @param reportProgress flag to report request and response progress.
   */
  public getAllUsers(observe?: 'body', reportProgress?: boolean): Observable<Array<UserDto>>;

  public getAllUsers(observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<UserDto>>>;

  public getAllUsers(observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<UserDto>>>;

  public getAllUsers(observe: any = 'body', reportProgress: boolean = false): Observable<any> {

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

    return this.httpClient.request<Array<UserDto>>('get', `${this.basePath}/api/v1/User`,
      {
        withCredentials: this.configuration.withCredentials,
        headers: headers,
        observe: observe,
        reportProgress: reportProgress
      }
    );
  }

  /**
   * Get user by ID
   *
   * @param userId ID of the user
   * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
   * @param reportProgress flag to report request and response progress.
   */
  public getUserById(userId: number, observe?: 'body', reportProgress?: boolean): Observable<UserDto>;

  public getUserById(userId: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<UserDto>>;

  public getUserById(userId: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<UserDto>>;

  public getUserById(userId: number, observe: any = 'body', reportProgress: boolean = false): Observable<any> {

    if (userId === null || userId === undefined) {
      throw new Error('Required parameter userId was null or undefined when calling getUserById.');
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

    return this.httpClient.request<UserDto>('get', `${this.basePath}/api/v1/User/${encodeURIComponent(String(userId))}`,
      {
        withCredentials: this.configuration.withCredentials,
        headers: headers,
        observe: observe,
        reportProgress: reportProgress
      }
    );
  }

  /**
   * Update an existing user
   *
   * @param userId ID of the user to be updated
   * @param body Updated user
   * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
   * @param reportProgress flag to report request and response progress.
   */
  public updateUser(userId: number, body?: UserDto, observe?: 'body', reportProgress?: boolean): Observable<UserDto>;

  public updateUser(userId: number, body?: UserDto, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<UserDto>>;

  public updateUser(userId: number, body?: UserDto, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<UserDto>>;

  public updateUser(userId: number, body?: UserDto, observe: any = 'body', reportProgress: boolean = false): Observable<any> {

    if (userId === null || userId === undefined) {
      throw new Error('Required parameter userId was null or undefined when calling updateUser.');
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

    return this.httpClient.request<UserDto>('put', `${this.basePath}/api/v1/User/${encodeURIComponent(String(userId))}`,
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
