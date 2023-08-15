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
using QuanLyMoiTruong.Common.Enums;
using QuanLyMoiTruong.Application.Request;

namespace QuanLyMoiTruong.Application.Services
{

    public class ThanhPhanMoiTruongService : IThanhPhanMoiTruongService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileTaiLieuService _fileTaiLieuService;

        public ThanhPhanMoiTruongService(IUnitOfWork unitOfWork, IFileTaiLieuService fileTaiLieuService)
        {
            _unitOfWork= unitOfWork;
            _fileTaiLieuService= fileTaiLieuService;
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var entity =  await _unitOfWork.GetRepository<ThanhPhanMoiTruong>().FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted= true;
                await _unitOfWork.SaveChangesAsync();
                return new ApiSuccessResult<bool>() {};
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa") ;
            }
        } 

        public async Task<ApiResult<IList<ThanhPhanMoiTruongViewModel>>> GetAll()
        {
            var result = new List<ThanhPhanMoiTruongViewModel>();
            var entities =  await _unitOfWork.GetRepository<ThanhPhanMoiTruong>().GetAllAsync(predicate: x => !x.IsDeleted);
            result = entities.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IList<ThanhPhanMoiTruongViewModel>>() { Data = result };
        }

        public async Task<ApiResult<ThanhPhanMoiTruongViewModel>> GetById(int id)
        {
            var result = new ThanhPhanMoiTruongViewModel();
            var data = await _unitOfWork.GetRepository<ThanhPhanMoiTruong>().GetFirstOrDefaultAsync(predicate: x => x.IdThanhPhanMoiTruong == id);
            result = MapEntityToViewModel(data);
            var dsFile = await _fileTaiLieuService.GetByTaiLieu(data.IdThanhPhanMoiTruong, NhomTaiLieuEnum.ThanhPhanMoiTruong.ToString());
            if (dsFile.Success)
            {
                result.FileTaiLieu = dsFile.Data.ToList();
            }
            return new ApiSuccessResult<ThanhPhanMoiTruongViewModel>() {Data = result };
        }

        public async Task<ApiResult<ThanhPhanMoiTruong>> Insert(ThanhPhanMoiTruongViewModel obj)
        {
            var entity = new ThanhPhanMoiTruong();
            entity = MapViewModelToEntity(obj, entity);
            await _unitOfWork.GetRepository<ThanhPhanMoiTruong>().InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            var fileTaiLieuRequest = new FileTaiLieuRequest();
            fileTaiLieuRequest.IdTaiLieu = entity.IdThanhPhanMoiTruong;
            fileTaiLieuRequest.NhomTaiLieu = NhomTaiLieuEnum.ThanhPhanMoiTruong.ToString();
            fileTaiLieuRequest.FileTaiLieu = obj.FileTaiLieu;
            await _fileTaiLieuService.UpdateAll(fileTaiLieuRequest);
            return new ApiSuccessResult<ThanhPhanMoiTruong>() { Data = entity };
        }

        public async Task<ApiResult<bool>> Remove(int id)
        {
            var entity = await _unitOfWork.GetRepository<ThanhPhanMoiTruong>().FindAsync(id);
            if (entity != null)
            {
                _unitOfWork.GetRepository<ThanhPhanMoiTruong>().Remove(id);
                return new ApiSuccessResult<bool>() { };
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa");
            }
        }

        public async Task<ApiResult<IPagedList<ThanhPhanMoiTruongViewModel>>> GetAllPaging(PagingRequest request)
        {
            Expression<Func<ThanhPhanMoiTruong, bool>> filter = x => true;
            if (!string.IsNullOrWhiteSpace(request.FullTextSearch))
            {
                var fullTextSearch = request.FullTextSearch.ToLowerInvariant();
                filter = filter.And(p => p.TenThanhPhanMoiTruong.ToLower().Contains(fullTextSearch));
            }
            filter = filter.And(p => !p.IsDeleted);
            var data = await _unitOfWork.GetRepository<ThanhPhanMoiTruong>().GetPagedListAsync(predicate: filter, pageIndex: request.PageIndex, pageSize: request.PageSize);
            data.Items.Select(MapEntityToViewModel);
            var result = new PagedList<ThanhPhanMoiTruongViewModel>();

            result.PageIndex = data.PageIndex;
            result.PageSize = data.PageSize;
            result.IndexFrom= data.IndexFrom;
            result.TotalCount = data.TotalCount;
            result.TotalPages = data.TotalPages;    
            result.Items = data.Items.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IPagedList<ThanhPhanMoiTruongViewModel>>() { Data = result };
        }

        public async Task<ApiResult<ThanhPhanMoiTruong>> Update(ThanhPhanMoiTruongViewModel obj)
        {
            var entity = await _unitOfWork.GetRepository<ThanhPhanMoiTruong>().GetFirstOrDefaultAsync(predicate: x => x.IdThanhPhanMoiTruong == obj.IdThanhPhanMoiTruong);
            entity = MapViewModelToEntity(obj, entity);
            _unitOfWork.GetRepository<ThanhPhanMoiTruong>().Update(entity);
            await _unitOfWork.SaveChangesAsync();

            var fileTaiLieuRequest = new FileTaiLieuRequest();
            fileTaiLieuRequest.IdTaiLieu = entity.IdThanhPhanMoiTruong;
            fileTaiLieuRequest.NhomTaiLieu = NhomTaiLieuEnum.ThanhPhanMoiTruong.ToString();
            fileTaiLieuRequest.FileTaiLieu = obj.FileTaiLieu;
            await _fileTaiLieuService.UpdateAll(fileTaiLieuRequest);

            return new ApiSuccessResult<ThanhPhanMoiTruong>() { Data = entity };
        }
        public ThanhPhanMoiTruongViewModel MapEntityToViewModel(ThanhPhanMoiTruong entity) {
            var result = new ThanhPhanMoiTruongViewModel();
            result.IdThanhPhanMoiTruong = entity.IdThanhPhanMoiTruong;
            result.TenThanhPhanMoiTruong = entity.TenThanhPhanMoiTruong;
            return result;
        }
        public ThanhPhanMoiTruong MapViewModelToEntity(ThanhPhanMoiTruongViewModel viewModel, ThanhPhanMoiTruong entity)
        {
            entity.IdThanhPhanMoiTruong = viewModel.IdThanhPhanMoiTruong;
            entity.TenThanhPhanMoiTruong = viewModel.TenThanhPhanMoiTruong;
            return entity;
        }
    }

}
