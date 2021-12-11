
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

## How To Use
```cs
// use the dashboard builder to start creating a dashboard
var dashboard = new DashboardBuilder()
                .WithName(name) // give it a name
                .WithBuilder(new DashboardPartsBuilder() // to add parts, use a DashboardPartsBuilder
                    .AddPart(new LogsDashboardPart() // this adds a LogsDashboardsPart (from Log Analytics)
                        .WithTitle("My First Logs Dashboard Part") 
                        .WithSubTitle("My First Subtitle")
                        .WithInsightsComponentName("HwEgWebAppJosh") // this is the name of the Application Insights log analytics resource
                        .WithQuery("requests\n| where success == false\n| render barchart\n") // the Kusto query you want to execute
                        .WithSubscriptionId(subscriptionId) // the subscriptipn ID of where this should be
                        .WithResourceGroup(resourceGroup) // the resource group of where this should be
                        .GeneratePart())) // Generates a DashboardPart to be given to the builder
                .Build(); Tells the DashboardBuilder to build the final Dashboard object

// Generate the JSON with just the main properties (useful if you just need the JSON and want to deploy it via CLI / REST / Other deployment methods (e.g. Terraform)
var dashboardPropertiesJson = Generator.Generate(dashboard.Properties);

// Generats JSON for whole dashboard:
var dashboardJson = Generator.Generate(dashboard);
```
_* currently Kustomaur does not support the ability to automatically create the dashboard in Azure, as it simply a JSON generation library - but this missing functionality is coming soon!!!!_
## Overview
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


