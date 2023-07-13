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

    public class BaoCaoBaoVeMoiTruongHangNamService : IBaoCaoBaoVeMoiTruongHangNamService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BaoCaoBaoVeMoiTruongHangNamService(IUnitOfWork unitOfWork)
        {
            _unitOfWork= unitOfWork;
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var entity =  await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruongHangNam>().FindAsync(id);
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

        public async Task<ApiResult<IList<BaoCaoBaoVeMoiTruongHangNamViewModel>>> GetAll()
        {
            var result = new List<BaoCaoBaoVeMoiTruongHangNamViewModel>();
            var entities =  await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruongHangNam>().GetAllAsync(predicate: x => !x.IsDeleted);
            result = entities.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IList<BaoCaoBaoVeMoiTruongHangNamViewModel>>() { Data = result };
        }

        public async Task<ApiResult<BaoCaoBaoVeMoiTruongHangNamViewModel>> GetById(int id)
        {
            var result = new BaoCaoBaoVeMoiTruongHangNamViewModel();
            var data = await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruongHangNam>().FindAsync(id);
            result = MapEntityToViewModel(data);
            return new ApiSuccessResult<BaoCaoBaoVeMoiTruongHangNamViewModel>() {Data = result };
        }

        public async Task<ApiResult<BaoCaoBaoVeMoiTruongHangNam>> Insert(BaoCaoBaoVeMoiTruongHangNamViewModel obj)
        {
            var entity = new BaoCaoBaoVeMoiTruongHangNam();
            entity = MapViewModelToEntity(obj);
            await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruongHangNam>().InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return new ApiSuccessResult<BaoCaoBaoVeMoiTruongHangNam>() { Data = entity };
        }

        public async Task<ApiResult<bool>> Remove(int id)
        {
            var entity = await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruongHangNam>().FindAsync(id);
            if (entity != null)
            {
                _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruongHangNam>().Remove(id);
                return new ApiSuccessResult<bool>() { };
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa");
            }
        }

        public async Task<ApiResult<IPagedList<BaoCaoBaoVeMoiTruongHangNamViewModel>>> GetAllPaging(BaoCaoBaoVeMoiTruongHangNamRequest request)
        {
            Expression<Func<BaoCaoBaoVeMoiTruongHangNam, bool>> filter = x => true;
            if (!string.IsNullOrWhiteSpace(request.FullTextSearch))
            {
                var fullTextSearch = request.FullTextSearch.ToLowerInvariant();
                filter = filter.And(p => p.TenBaoCao.ToLower().Contains(fullTextSearch));
            }
            filter = filter.And(p => !p.IsDeleted);
            var data = await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruongHangNam>().GetPagedListAsync(predicate: filter, pageIndex: request.PageIndex, pageSize: request.PageSize);
            data.Items.Select(MapEntityToViewModel);
            var result = new PagedList<BaoCaoBaoVeMoiTruongHangNamViewModel>();

            result.PageIndex = data.PageIndex;
            result.PageSize = data.PageSize;
            result.IndexFrom= data.IndexFrom;
            result.TotalCount = data.TotalCount;
            result.TotalPages = data.TotalPages;    
            result.Items = data.Items.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IPagedList<BaoCaoBaoVeMoiTruongHangNamViewModel>>() { Data = result };
        }

        public Task<ApiResult<BaoCaoBaoVeMoiTruongHangNam>> Update(BaoCaoBaoVeMoiTruongHangNamViewModel obj)
        {
            throw new NotImplementedException();
        }
        public BaoCaoBaoVeMoiTruongHangNamViewModel MapEntityToViewModel(BaoCaoBaoVeMoiTruongHangNam entity) {
            var result = new BaoCaoBaoVeMoiTruongHangNamViewModel();
            result.IdBaoCaoBaoVeMoiTruongHangNam = entity.IdBaoCaoBaoVeMoiTruongHangNam;
            result.TenBaoCao = entity.TenBaoCao;
            result.NgayBaoCao = entity.NgayBaoCao != null ? entity.NgayBaoCao.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "";
            result.IdDuAn = entity.IdDuAn;
            result.TenDuAn = entity.DuAn.TenDuAn;
            result.TenDoanhNghiep = entity.DuAn.TenDoanhNghiep;
            return result;
        }
        public BaoCaoBaoVeMoiTruongHangNam MapViewModelToEntity(BaoCaoBaoVeMoiTruongHangNamViewModel viewModel)
        {
            var entity = new BaoCaoBaoVeMoiTruongHangNam();
            entity.IdBaoCaoBaoVeMoiTruongHangNam = viewModel.IdBaoCaoBaoVeMoiTruongHangNam;
            entity.TenBaoCao = viewModel.TenBaoCao;
            entity.NgayBaoCao = string.IsNullOrEmpty(viewModel.NgayBaoCao) ? null : DateTime.Parse(viewModel.NgayBaoCao, new CultureInfo("vi-VN"));
            entity.IdDuAn = viewModel.IdDuAn;

            return entity;
        }
    }

}
