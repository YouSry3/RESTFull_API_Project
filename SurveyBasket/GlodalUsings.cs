

global using FluentValidation;
global using FluentValidation.AspNetCore;
global using Mapster;
global using MapsterMapper;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Options;
global using Microsoft.IdentityModel.Tokens;
global using SurveyBasket.Abstractions;
global using SurveyBasket.Contract.Answers;
global using SurveyBasket.Contract.Authentication;
global using SurveyBasket.Contract.Polls;
global using SurveyBasket.Entities;
global using SurveyBasket.EntitiesConfigurations;
global using SurveyBasket.Errors;
global using SurveyBasket.Middelware;
global using SurveyBasket.Persistence;
global using SurveyBasket.Services;
global using SurveyBasket.Services.Authentication;
global using SurveyBasket.Services.Polls;
global using System.IdentityModel.Tokens.Jwt;
global using System.Reflection;
global using System.Security.Claims;
global using System.Diagnostics.Contracts;

global using SurveyBasket.Contract.Questions;
global using SurveyBasket.Services.Questions;