﻿using DemoApp.Application.Common.Security.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public record DeleteEmployeeCommand(Guid Id) :  IRequest;
