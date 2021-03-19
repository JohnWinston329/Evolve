# Evolve [![Build status](https://ci.appveyor.com/api/projects/status/oj9wf4bk0p0npggu/branch/master?svg=true)](https://ci.appveyor.com/project/lecaillon/evolve) [![Build Status](https://lecaillon.visualstudio.com/Evolve-CI/_apis/build/status/Evolve-CI?branchName=master)](https://lecaillon.visualstudio.com/Evolve-CI/_build/latest?definitionId=2&branchName=master) [![Build status](https://img.shields.io/azure-devops/coverage/lecaillon/evolve-ci/2/master.svg?color=brightgreen)](https://lecaillon.visualstudio.com/Evolve-CI/_build/latest?definitionId=2&branchName=master)
<img align="right" width="173px" height="173px" src="https://raw.githubusercontent.com/lecaillon/Evolve/master/images/logo.png">

Evolve is a cross platform database migration tool inspired by [Flyway](https://flywaydb.org/), that uses plain SQL scripts.

Its purpose is to automate your database changes, and help keep those changes synchronized through all your environments and development teams. This makes it an ideal tool for continuous integration / delivery.

Overall Evolve embraces simplicity. Every time you run your project, it will automatically ensure that your database is up-to-date. Install it and forget it!

### Supporting Evolve

Evolve is a personal, free-time project with no funding. If you use Evolve in your daily work and feel that it makes your life easier, consider supporting its development via [GitHub Sponsors](https://github.com/sponsors/lecaillon) :heart: and by adding a star to this repository :star:

I’m very passionate about doing personal projects and very grateful that other people find them useful, too. I want to share as much as possible, but it doesn’t pay the bills. With your help, I can focus on open-source work more and make it sustainable. If your company uses any of my libraries, consider donating too as a sign of gratitude and for priority support!

## Installation
Evolve is available as a .NET library, a .NET tool and a standalone CLI.

|  | Evolve | Evolve Tool | Evolve CLI |
|-|-|-|-|
| Repository | [![NuGet](https://img.shields.io/nuget/dt/evolve)](https://www.nuget.org/packages/Evolve) | [![NuGet](https://img.shields.io/nuget/dt/Evolve.Tool)](https://www.nuget.org/packages/Evolve.Tool) | [![NuGet](https://img.shields.io/badge/GitHub-releases-brightgreen.svg?logo=github)](https://github.com/lecaillon/Evolve/releases) |

## Supported databases
<table>
  <tbody>
    <tr>
      <td align="center" valign="middle">
        <a href="https://evolve-db.netlify.com/requirements/sqlserver" target="_blank">
          <img src="https://raw.githubusercontent.com/lecaillon/Evolve/master/images/sqlserver.png">
        </a>
      </td>
      <td align="center" valign="middle">
        <a href="https://evolve-db.netlify.com/requirements/postgresql" target="_blank">
          <img src="https://raw.githubusercontent.com/lecaillon/Evolve/master/images/postgresql.png">
        </a>
      </td>
      <td align="center" valign="middle">
        <a href="https://evolve-db.netlify.com/requirements/mysql" target="_blank">
          <img src="https://raw.githubusercontent.com/lecaillon/Evolve/master/images/mysql.png">
        </a>
      </td>
      <td align="center" valign="middle">
        <a href="https://evolve-db.netlify.com/requirements/mariadb" target="_blank">
          <img src="https://raw.githubusercontent.com/lecaillon/Evolve/master/images/mariadb.png">
        </a>
      </td>
      <td align="center" valign="middle">
        <a href="https://evolve-db.netlify.com/requirements/sqlite" target="_blank">
          <img src="https://raw.githubusercontent.com/lecaillon/Evolve/master/images/sqlite.png">
        </a>
      </td>
      <td align="center" valign="middle">
        <a href="https://evolve-db.netlify.com/requirements/cassandra" target="_blank">
          <img src="https://raw.githubusercontent.com/lecaillon/Evolve/master/images/cassandra.png">
        </a>
      </td>
      <td align="center" valign="middle">
        <a href="https://evolve-db.netlify.com/requirements/cockroachdb" target="_blank">
          <img src="https://raw.githubusercontent.com/lecaillon/Evolve/master/images/cockroachdb.png">
        </a>
      </td>
    </tr>
  </tbody>
</table>

## Documentation
You can read the latest documentation at [https://evolve-db.netlify.com](https://evolve-db.netlify.com) and find samples [here](https://github.com/lecaillon/Evolve/tree/master/samples).

## Changelog
Detailed changes for each release are documented in the [release notes](https://github.com/lecaillon/Evolve/releases).
