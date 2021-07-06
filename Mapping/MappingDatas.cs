using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Project5.Model;
using Project5.Models;



namespace Project5.Mapping
{
    public class MappingDatas:Profile
    {
        public MappingDatas()
        {
            CreateMap<ClassLibrary4.Model.Person, Project5.Model.Person>();
            CreateMap<ClassLibrary4.Model.PersonDataOutput, PersonDataOutput>();
       //     CreateMap<ClassLibrary4.Model.Person, PersonDataOutput>().ForMember(d => d.Id,, s => s.MapFrom(s => s.Id));
       //     CreateMap<ClassLibrary4.Model.Person, Person>().ForMember(a=>a.Id, s=>s.MapFrom(s=>s.))
        }
    }
}
