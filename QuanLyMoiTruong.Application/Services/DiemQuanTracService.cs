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

    public class DiemQuanTracService : IDiemQuanTracService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileTaiLieuService _fileTaiLieuService;

        public DiemQuanTracService(IUnitOfWork unitOfWork, IFileTaiLieuService fileTaiLieuService)
        {
            _unitOfWork= unitOfWork;
            _fileTaiLieuService= fileTaiLieuService;
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var entity =  await _unitOfWork.GetRepository<DiemQuanTrac>().FindAsync(id);
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

        public async Task<ApiResult<IList<DiemQuanTracViewModel>>> GetAll()
        {
            var result = new List<DiemQuanTracViewModel>();
            var entities =  await _unitOfWork.GetRepository<DiemQuanTrac>().GetAllAsync(predicate: x => !x.IsDeleted);
            result = entities.Select(MapEntityToViewModel).OrderBy(x=> x.Loai).ThenBy(x=> x.TenDiemQuanTrac).ToList();
            return new ApiSuccessResult<IList<DiemQuanTracViewModel>>() { Data = result };
        }

        public async Task<ApiResult<DiemQuanTracViewModel>> GetById(int id)
        {
            var result = new DiemQuanTracViewModel();
            var data = await _unitOfWork.GetRepository<DiemQuanTrac>().GetFirstOrDefaultAsync(predicate: x => x.IdDiemQuanTrac == id);
            result = MapEntityToViewModel(data);
            return new ApiSuccessResult<DiemQuanTracViewModel>() {Data = result };
        }

        public async Task<ApiResult<DiemQuanTrac>> Insert(DiemQuanTracViewModel obj)
        {
            var entity = new DiemQuanTrac();
            entity = MapViewModelToEntity(obj, entity);
            await _unitOfWork.GetRepository<DiemQuanTrac>().InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return new ApiSuccessResult<DiemQuanTrac>() { Data = entity };
        }

        public async Task<ApiResult<bool>> Remove(int id)
        {
            var entity = await _unitOfWork.GetRepository<DiemQuanTrac>().FindAsync(id);
            if (entity != null)
            {
                _unitOfWork.GetRepository<DiemQuanTrac>().Remove(id);
                return new ApiSuccessResult<bool>() { };
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa");
            }
        }

        public async Task<ApiResult<IPagedList<DiemQuanTracViewModel>>> GetAllPaging(PagingRequest request)
        {
            Expression<Func<DiemQuanTrac, bool>> filter = x => true;
            if (!string.IsNullOrWhiteSpace(request.FullTextSearch))
            {
                var fullTextSearch = request.FullTextSearch.ToLowerInvariant();
                filter = filter.And(p => p.TenDiemQuanTrac.ToLower().Contains(fullTextSearch));
            }
            filter = filter.And(p => !p.IsDeleted);
            var data = await _unitOfWork.GetRepository<DiemQuanTrac>().GetPagedListAsync(predicate: filter, pageIndex: request.PageIndex, pageSize: request.PageSize);
            data.Items.Select(MapEntityToViewModel);
            var result = new PagedList<DiemQuanTracViewModel>();

            result.PageIndex = data.PageIndex;
            result.PageSize = data.PageSize;
            result.IndexFrom= data.IndexFrom;
            result.TotalCount = data.TotalCount;
            result.TotalPages = data.TotalPages;    
            result.Items = data.Items.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IPagedList<DiemQuanTracViewModel>>() { Data = result };
        }

        public async Task<ApiResult<DiemQuanTrac>> Update(DiemQuanTracViewModel obj)
        {
            var entity = await _unitOfWork.GetRepository<DiemQuanTrac>().GetFirstOrDefaultAsync(predicate: x => x.IdDiemQuanTrac == obj.IdDiemQuanTrac);
            entity = MapViewModelToEntity(obj, entity);
            _unitOfWork.GetRepository<DiemQuanTrac>().Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return new ApiSuccessResult<DiemQuanTrac>() { Data = entity };
        }
        public async Task<ApiResult<IList<DiemQuanTracViewModel>>> GetDuLieuLenBanDo(int idThanhPhanMoiTruong)
        {
            var result = new List<DiemQuanTracViewModel>();
            var entities = await _unitOfWork.GetRepository<DiemQuanTrac>().GetAllAsync(predicate: x => !x.IsDeleted);
            result = entities.Select(MapEntityToViewModel).OrderBy(x => x.Loai).ThenBy(x => x.TenDiemQuanTrac).ToList();
            foreach(var item in result)
            {
                var dsKetQuaQuanTrac = await _unitOfWork.GetRepository<KetQuaQuanTrac>().GetAllAsync(predicate: x => x.IdDiemQuanTrac == item.IdDiemQuanTrac && x.IdThanhPhanMoiTruong == idThanhPhanMoiTruong);
                item.DsKetQuaQuanTrac = dsKetQuaQuanTrac.ToList();
            }
            return new ApiSuccessResult<IList<DiemQuanTracViewModel>>() { Data = result };
        }
        public DiemQuanTracViewModel MapEntityToViewModel(DiemQuanTrac entity) {
            var result = new DiemQuanTracViewModel();
            result.IdDiemQuanTrac = entity.IdDiemQuanTrac;
            result.TenDiemQuanTrac = entity.TenDiemQuanTrac;
            result.DiaChi = entity.DiaChi;
            result.Loai = entity.Loai;
            result.KinhDo = entity.KinhDo;
            result.ViDo = entity.ViDo;
            return result;
        }
        public DiemQuanTrac MapViewModelToEntity(DiemQuanTracViewModel viewModel, DiemQuanTrac entity)
        {
            entity.IdDiemQuanTrac = viewModel.IdDiemQuanTrac;
            entity.TenDiemQuanTrac = viewModel.TenDiemQuanTrac;
            entity.DiaChi = viewModel.DiaChi;
            entity.Loai = viewModel.Loai;
            entity.KinhDo = viewModel.KinhDo;
            entity.ViDo = viewModel.ViDo;
            return entity;
        }
    }

}
