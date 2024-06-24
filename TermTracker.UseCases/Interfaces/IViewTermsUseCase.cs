﻿using TermTracker.CoreBusiness.Models;

namespace TermTracker.UseCases.Interfaces;
public interface IViewTermsUseCase
{
    Task<List<Term>> ExecuteAsync();
}