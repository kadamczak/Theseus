﻿using AutoMapper;
using Theseus.Domain.Models.ExamSetRelated;
using Theseus.Infrastructure.DbContexts;
using Theseus.Infrastructure.Dtos;

namespace Theseus.Infrastructure.Queries.ExamSetQueries
{
    /// <summary>
    /// Abstract query class, with method for <c>ExamSetDto</c> to <c>ExamSet</c> mapping.
    /// </summary>
    public abstract class ExamSetQuery : Query
    {
        protected ExamSetQuery(TheseusDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
        {
        }

        protected List<ExamSet> MapExamSets(IEnumerable<ExamSetDto> examSetDtos)
        {
            List<ExamSet> examSets = new List<ExamSet>();

            if (examSetDtos is null)
                return examSets;

            foreach (var examSet in examSetDtos)
            {
                examSets.Add(MapExamSet(examSet));
            }
            return examSets;
        }

        protected ExamSet MapExamSet(ExamSetDto examSetDto)
        {
            ExamSet examSet = new ExamSet();
            Mapper.Map(examSetDto, examSet);
            return examSet;
        }
    }
}