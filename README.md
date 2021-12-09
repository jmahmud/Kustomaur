
# Kustomaur 

![Release Branch CI Status](https://github.com/jmahmud/Kustomaur/actions/workflows/dotnet.yml/badge.svg?branch=release)

A fluent dotnet/core SDK to generate Azure Dashboards

![Code to Dashboard](https://raw.githubusercontent.com/jmahmud/Kustomaur/feature/documentation/images/CodeToDashboardImage.png)

## Contents

* [Nuget](#Nuget)
* [Getting Started](#getting-started)
* [Features](#features)
  * [Dashboard Parts](#dashboard-parts)
  * [Adding Rows/Columns](#adding-rowscolumns)
* [Outstanding/ToDo](#outstandingtodo)
* [Why?](#why)

## Nuget
Kustomaur.Builder is available on Nuget: https://www.nuget.org/packages/Kustomaur.Builder/

## Getting Started
Kustomaur is built with 2 layers:
* **Kustomaur.Models**: POCOs representing the JSON classes in Azure Dashboards
* **Kustomaur.Builders**: A set of fluent builders using higher level concepts to create & configure Azure Dashboards, which then produces a Kustomaur.Models.Dashboard object  

## Features
### Dashboard Parts

### Adding Rows/Columns

## Outstanding/ToDo 

## Why?
Currently, it is suggested (here: [Azure Portal Dashboards Create Programmatically](https://docs.microsoft.com/en-us/azure/azure-portal/azure-portal-dashboards-create-programmatically)), that the programmatic generation of an Azure Dashboard starts off with creating the dashboard by hand within the Azure Portal, exporting it as JSON, converting to a template (with parameters by hand), and then using a deployment method via CLI/Powershell/REST API  to upload the template.

Whilst this is a good idea for initially creating a dashboard, this becomes difficult to maintain when wanting to create small changes, as this process has to be repeated:
* prone to error when converting JSON to a template with parameters by hand
* reviews of large blobs of JSON is difficult
* a small change (like adding a new part), can cause lots of changes across the whole dashboard which is difficult to maintain
* not fully automated


