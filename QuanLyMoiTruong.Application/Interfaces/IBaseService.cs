using QuanLyMoiTruong.Application.ViewModels;
using QuanLyMoiTruong.UnitOfWork.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyMoiTruong.Application.Interfaces
{
    public interface IBaseService<TEntity, TEntityIdType, TViewModel, TRequest> /*where TRequest : PagingRequest*/
    {
        Task<ApiResult<IPagedList<TViewModel>>> GetAllPaging(TRequest request);

        Task<ApiResult<IList<TViewModel>>> GetAll();

        Task<ApiResult<TViewModel>> GetById(TEntityIdType id);

        Task<ApiResult<TEntity>> Insert(TViewModel obj);

        Task<ApiResult<TEntity>> Update(TViewModel obj);

        Task<ApiResult<bool>> Delete(TEntityIdType id);
        
        Task<ApiResult<bool>> Remove(TEntityIdType id);
    }
}
