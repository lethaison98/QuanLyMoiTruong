using QuanLyMoiTruong.Application.Interfaces;
using QuanLyMoiTruong.Data.Entities;
using QuanLyMoiTruong.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using QuanLyMoiTruong.Application.ViewModels;
using QuanLyMoiTruong.UnitOfWork.Collections;
using System;
using System.Globalization;
using System.Reflection.Metadata;
using QuanLyMoiTruong.Application.Requests;
using System.Linq.Expressions;
using QuanLyMoiTruong.Common.Expressions;

namespace QuanLyMoiTruong.Application.Services
{

    public class KhuCongNghiepService : IKhuCongNghiepService
    {
        private readonly IUnitOfWork _unitOfWork;
        public KhuCongNghiepService(IUnitOfWork unitOfWork)
        {
            _unitOfWork= unitOfWork;
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var entity =  await _unitOfWork.GetRepository<KhuCongNghiep>().FindAsync(id);
            if (entity == null)
            {
                entity.IsDeleted= true;
                return new ApiSuccessResult<bool>() {};
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa") ;
            }
        } 

        public async Task<ApiResult<IList<KhuCongNghiep>>> GetAll()
        {
            var result =  await _unitOfWork.GetRepository<KhuCongNghiep>().GetAllAsync(predicate: x => !x.IsDeleted);
            return new ApiSuccessResult<IList<KhuCongNghiep>>() { Data = result };
        }

        public async Task<ApiResult<KhuCongNghiep>> GetById(int id)
        {
            var result = await _unitOfWork.GetRepository<KhuCongNghiep>().FindAsync(id);
            return new ApiSuccessResult<KhuCongNghiep>() {Data = result };
        }

        public async Task<ApiResult<KhuCongNghiep>> Insert(KhuCongNghiep obj)
        {
            await _unitOfWork.GetRepository<KhuCongNghiep>().InsertAsync(obj);
            await _unitOfWork.SaveChangesAsync();
            return new ApiSuccessResult<KhuCongNghiep>() { Data = obj };
        }

        public async Task<ApiResult<bool>> Remove(int id)
        {
            var entity = await _unitOfWork.GetRepository<KhuCongNghiep>().FindAsync(id);
            if (entity == null)
            {
                _unitOfWork.GetRepository<KhuCongNghiep>().Remove(id);
                return new ApiSuccessResult<bool>() { };
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa");
            }
        }

        public async Task<ApiResult<IPagedList<KhuCongNghiep>>> GetAllPaging(PagingRequest request)
        {
            Expression<Func<KhuCongNghiep, bool>> filter = x => true;
            if (!string.IsNullOrWhiteSpace(request.FullTextSearch))
            {
                var fullTextSearch = request.FullTextSearch.ToLowerInvariant();
                filter = filter.And(p => p.TenKhuCongNghiep.ToLower().Contains(fullTextSearch));
            }

            var data = await _unitOfWork.GetRepository<KhuCongNghiep>().GetPagedListAsync(predicate: filter, pageIndex: request.PageIndex, pageSize: request.PageSize);
            var result = new PagedList<KhuCongNghiep>();

            result.PageIndex = data.PageIndex;
            result.PageSize = data.PageSize;
            result.IndexFrom= data.IndexFrom;
            result.TotalCount = data.TotalCount;
            result.TotalPages = data.TotalPages;    
            result.Items = data.Items;
            return new ApiSuccessResult<IPagedList<KhuCongNghiep>>() { Data = result };
        }

        public Task<ApiResult<KhuCongNghiep>> Update(KhuCongNghiep obj)
        {
            throw new NotImplementedException();
        }
       
    }

}
