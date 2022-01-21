
# Kustomaur 

![Release Branch CI Status](https://github.com/jmahmud/Kustomaur/actions/workflows/dotnet.yml/badge.svg?branch=release)

A fluent dotnet/core SDK to generate Azure Dashboards

![Code to Dashboard](https://raw.githubusercontent.com/jmahmud/Kustomaur/main/images/CodeToDashboardImage.png)

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
_* currently Kustomaur does not support the ability to automatically create the dashboard in Azure, as it simply a JSON generation library - but this missing functionality is coming soon in Kustomaur.Azure!!!!_
## Overview
Kustomaur is built with 2 layers:
* **Kustomaur.Models**: POCOs representing the JSON classes in Azure Dashboards
* **Kustomaur.Builders**: A set of fluent builders using higher level concepts to create & configure Azure Dashboards, which then produces a Kustomaur.Models.Dashboard object

_A third layer of **Kustomaur.Azure** will add further Azure Portal functionality to be able to create the dashboard and make it available in the portal_

## Features
### Dashboard Parts / Part Types
Current Dashboard Parts that are supported:

|Name|Description|Example(s)|
|----|-----------|---|
|ApplicationMapPart|Generates an application map for the ApplicationInsights ID that is set for this part|[ApplicationMapTests](https://github.com/jmahmud/Kustomaur/blob/main/tests/Kustomaur.Builder.Tests/DashboardParts/ApplicationMapPartTests.cs)|
|LogsDashboardPart|This is a part that can be added to dashboard that stems from a Azure Log Analytics - a Kusto query can be set as well as defining the dimensions and rendering options|[LogDashboardTests](https://github.com/jmahmud/Kustomaur/blob/main/tests/Kustomaur.Builder.Tests/DashboardParts/LogsDashboardPartTests.cs)|
|MarkdownPart|Part which allows to create markdown|[Simple Dashboard with Markdown](https://github.com/jmahmud/Kustomaur/blob/db97d22fe20f5f1eca947bb38bc2e6f01ec61d0f/tests/Kustomaur.Builder.Tests/DashboardBuilderTests.cs#L35)|
|MonitorChartPart|This part allows the display of a Monitor chart, for example the machine stats in a serverFarm|[MonitorChartPartTests](https://github.com/jmahmud/Kustomaur/blob/main/tests/Kustomaur.Builder.Tests/DashboardParts/MonitorChartPartTests.cs)|

Other parts will be supported in future, but for ones that are not, new ones can be easily added by inheriting: [DashboardPart](https://github.com/jmahmud/Kustomaur/blob/main/src/Kustomaur.Builder/DashboardParts/DashboardPart.cs) class.
### Adding Parts in Rows/Columns
Adding multiple parts in a single row or column is straight forward with Kustomaur.  

In order to add multiple parts in a single row can be done as a single list:
```cs
  var dashboard = new DashboardBuilder()
  .WithSubscription(subscriptionId)
  .WithResourceGroup(resourceGroup)
  .WithName(name)
  .WithBuilder(new DashboardPartsBuilder()
      .AddPartsAsRow(new List<Part>()
      {
          new MarkdownPart(content:"# my first part in 1st row")
              .WithColSpan(3)
              .GeneratePart(),
          new MarkdownPart(content:"# my second part in 1st row")
              .WithColSpan(3)
              .GeneratePart()
      })
      .AddPartsAsRow(new List<Part>()
      {
          new MarkdownPart(content:"# my first part in 2nd row")
              .WithColSpan(4)
              .GeneratePart(),
          new MarkdownPart(content:"# my second part in 2nd row")
              .WithColSpan(4)
              .GeneratePart()
      })
  .Build();
```
The same can be done as adding multiple parts in a column:
```cs
var dashboard = new DashboardBuilder()
    .WithSubscription(subscriptionId)
    .WithResourceGroup(resourceGroup)
    .WithName(name)
    .WithBuilder(new DashboardPartsBuilder()
        .AddPartsAsColumn(new List<Part>()
        {
            new MarkdownPart(content:"# my first part in 1st column")
                .WithRowSpan(3)
                .GeneratePart(),
            new MarkdownPart(content:"# my second part in 1st column")
                .WithRowSpan(3)
                .GeneratePart()
        }) 
        .AddPartsAsColumn(new List<Part>()
        {
            new MarkdownPart(content:"# my first part in 2nd column")
                .WithRowSpan(4)
                .GeneratePart(),
            new MarkdownPart(content:"# my second part in 2nd column")
                .WithRowSpan(4)
                .GeneratePart()
        })
    .Build();
```

This can also be done in combination:
```cs
 var dashboard = new DashboardBuilder()
                .WithSubscription(subscriptionId)
                .WithResourceGroup(resourceGroup)
                .WithName(name)
                .WithBuilder(new DashboardPartsBuilder()
                    .AddPartsAsColumn(new List<Part>()
                    {
                        new MarkdownPart(content:"# my first part in 1st column")
                            .WithRowSpan(3).WithColSpan(2)
                            .GeneratePart(),
                        new MarkdownPart(content:"# my second part in 1st column")
                            .WithRowSpan(3).WithColSpan(2)
                            .GeneratePart()
                    }, out int firstColumnMaxX) // firstRowMaxY is what we should set the new row Y value
                    .AddPartsAsColumn(new List<Part>()
                    {
                        new MarkdownPart(content:"# my first part in 2nd column")
                            .WithRowSpan(4).WithColSpan(2)
                            .GeneratePart(),
                        new MarkdownPart(content:"# my second part in 2nd column")
                            .WithRowSpan(4).WithColSpan(2)
                            .GeneratePart()
                    }, out int secondColumnMaxX, startXPos:firstColumnMaxX)
                    .AddPartsAsColumn(new List<Part>()
                    {
                        new MarkdownPart(content:"# my first part in 3rd column")
                            .WithRowSpan(5).WithColSpan(2)
                            .GeneratePart(),
                        new MarkdownPart(content:"# my second part in 3rd column")
                            .WithRowSpan(5).WithColSpan(2)
                            .GeneratePart()
                    }, out int thirdColumnMaxX, startXPos:secondColumnMaxX)
                    .AddPartsAsRow(new List<Part>()
                    {
                        new MarkdownPart(content: "# my first part in the 4th column - as 1st row")
                            .WithX(thirdColumnMaxX)
                            .WithRowSpan(10).WithColSpan(6)
                            .GeneratePart(),
                        new MarkdownPart(content: "# my second part in the 5th column - as 1st row")
                            .WithX(thirdColumnMaxX)
                            .WithRowSpan(10).WithColSpan(6)
                            .GeneratePart()
                    }, out int firstRowMaxY)
                    .AddPartsAsRow(new List<Part>()
                    {
                        new MarkdownPart(content: "# my first part in the 4th column - as 2nd row")
                            .WithX(thirdColumnMaxX)
                            .WithRowSpan(10).WithColSpan(6)
                            .GeneratePart(),
                        new MarkdownPart(content: "# my second part in the 5th column - as 2nd row")
                            .WithX(thirdColumnMaxX)
                            .WithRowSpan(10).WithColSpan(6)
                            .GeneratePart()
                    }, out int secondRowMaxY, startYPos: firstRowMaxY)
                )
                .Build();
```
You will notice that `AddPartsAsRow()` has an overload which has an out parameter, which is set to the large Y position out of all of the parts in the row.  This value can be used incase new parts were to be added underneath the row (not using another `AddPartAsRow()`).  The same logic applies to `AddPArtsAsColumns` where the out parameter is set to the value of the component with the largest width/X value.

## Why?
Currently, it is suggested (here: [Azure Portal Dashboards Create Programmatically](https://docs.microsoft.com/en-us/azure/azure-portal/azure-portal-dashboards-create-programmatically)), that the programmatic generation of an Azure Dashboard starts off with creating the dashboard by hand within the Azure Portal, exporting it as JSON, converting to a template (with parameters by hand), and then using a deployment method via CLI/Powershell/REST API  to upload the template.

Whilst this is a good idea for initially creating a dashboard, this becomes difficult to maintain when wanting to create small changes, as this process has to be repeated:
* prone to error when converting JSON to a template with parameters by hand
* reviews of large blobs of JSON is difficult
* a small change (like adding a new part), can cause lots of changes across the whole dashboard which is difficult to maintain
* not fully automated


