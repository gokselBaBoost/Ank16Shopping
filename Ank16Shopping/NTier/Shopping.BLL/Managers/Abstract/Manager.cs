using AutoMapper;
using AutoMapper.EquivalencyExpression;
using AutoMapper.Extensions.ExpressionMapping;
using Shopping.DAL.Services.Abstract;
using Shopping.DTO;
using Shopping.Entities.Abstract;
using Shopping.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.BLL.Managers.Abstract
{
    public abstract class Manager<TDto, TViewModel, TEntity> : IManager<TDto, TViewModel>
       where TDto : BaseDto
       where TEntity : BaseEntity
       where TViewModel : BaseViewModel
    {
        protected Service<TEntity, TDto> _service;
        protected IMapper _mapper;

        protected Manager(Service<TEntity, TDto> service)
        {
            MapperConfiguration _config = new MapperConfiguration(cfg =>
            {
                cfg.AddExpressionMapping().AddCollectionMappers();
                cfg.CreateMap<TDto, TViewModel>().ReverseMap();
            });

            _mapper = _config.CreateMapper();

            _service = service;
        }

        public IMapper Mapper
        {
            set { _mapper = value; }
        }

        public int Add(TViewModel viewModel)
        {
            TDto dto = _mapper.Map<TDto>(viewModel);

            return _service.Add(dto);
        }

        public int Delete(TViewModel viewModel)
        {
            TDto dto = _mapper.Map<TDto>(viewModel);

            return _service.Delete(dto);
        }

        public TViewModel? Get(int id)
        {
            TDto? dto = _service.Get(id);

            return _mapper.Map<TViewModel>(dto);
        }

        public virtual IEnumerable<TViewModel> GetAll()
        {
            IEnumerable<TDto> list = _service.GetAll().ToList();

            return _mapper.Map<IEnumerable<TViewModel>>(list);
        }

        public int Update(TViewModel viewModel)
        {
            TDto dto = _mapper.Map<TDto>(viewModel);

            return _service.Update(dto);
        }
    }
}
