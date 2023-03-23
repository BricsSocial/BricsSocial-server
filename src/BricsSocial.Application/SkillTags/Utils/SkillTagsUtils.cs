using BricsSocial.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Application.SkillTags.Utils
{
    internal static class SkillTagsUtils
    {
        public static bool BeListOfTags(string skillTags)
        {
            var tags = skillTags.Split(",");
            return
                skillTags.Length >= SkillTag.Invariants.SkillTagsMinLength && skillTags.Length <= SkillTag.Invariants.SkillTagsMaxLength
                && tags.Length <= SkillTag.Invariants.SkillTagsMaxCount
                && tags.All(t => t.Length >= SkillTag.Invariants.SkillTagMinLength && t.Length <= SkillTag.Invariants.SkillTagMaxLength);
        }

        public static string InvalidTagsMessage()
        {
            return $"Skill tags string was invalid. It must be comma separated list of tags, each [{SkillTag.Invariants.SkillTagMinLength}, {SkillTag.Invariants.SkillTagMaxLength}] length, maximum {SkillTag.Invariants.SkillTagsMaxCount} tags";
        }

        public static List<string> ParseTags(string skillTags)
        {
            return skillTags == null ? new List<string>() : skillTags.Split(",").ToList();
        }

        public static bool MatchTags(string requestedTags, string skillTags)
        {
            var requestedTagsList = ParseTags(requestedTags);
            var match = false;
            foreach (var requestedTag in requestedTagsList)
            {
                match |= EF.Functions.Like(skillTags, $"%{requestedTag}%");
            }

            return match;
        }

        // TODO: DRY
        public static IQueryable<Specialist> MatchTags(this IQueryable<Specialist> source, string requestedTags)
        {
            var requestedTagsList = ParseTags(requestedTags);
            
            foreach (var requestedTag in requestedTagsList)
                source = source.Where(s => EF.Functions.Like(s.SkillTags, $"%{requestedTag}%"));

            return source;
        }

        public static IQueryable<Vacancy> MatchTags(this IQueryable<Vacancy> source, string requestedTags)
        {
            var requestedTagsList = ParseTags(requestedTags);

            foreach (var requestedTag in requestedTagsList)
                source = source.Where(s => EF.Functions.Like(s.SkillTags, $"%{requestedTag}%"));

            return source;
        }
    }
}
