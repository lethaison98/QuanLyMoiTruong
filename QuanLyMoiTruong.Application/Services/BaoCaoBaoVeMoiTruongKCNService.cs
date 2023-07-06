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

    public class BaoCaoBaoVeMoiTruongKCNService : IBaoCaoBaoVeMoiTruongKCNService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BaoCaoBaoVeMoiTruongKCNService(IUnitOfWork unitOfWork)
        {
            _unitOfWork= unitOfWork;
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var entity =  await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruongKCN>().FindAsync(id);
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

        public async Task<ApiResult<IList<BaoCaoBaoVeMoiTruongKCNViewModel>>> GetAll()
        {
            var result = new List<BaoCaoBaoVeMoiTruongKCNViewModel>();
            var entities =  await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruongKCN>().GetAllAsync(predicate: x => x.IsDeleted);
            result = entities.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IList<BaoCaoBaoVeMoiTruongKCNViewModel>>() { Data = result };
        }

        public async Task<ApiResult<BaoCaoBaoVeMoiTruongKCNViewModel>> GetById(int id)
        {
            var result = new BaoCaoBaoVeMoiTruongKCNViewModel();
            var data = await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruongKCN>().FindAsync(id);
            result = MapEntityToViewModel(data);
            return new ApiSuccessResult<BaoCaoBaoVeMoiTruongKCNViewModel>() {Data = result };
        }

        public async Task<ApiResult<BaoCaoBaoVeMoiTruongKCN>> Insert(BaoCaoBaoVeMoiTruongKCNViewModel obj)
        {
            var entity = new BaoCaoBaoVeMoiTruongKCN();
            entity = MapViewModelToEntity(obj);
            await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruongKCN>().InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return new ApiSuccessResult<BaoCaoBaoVeMoiTruongKCN>() { Data = entity };
        }

        public async Task<ApiResult<bool>> Remove(int id)
        {
            var entity = await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruongKCN>().FindAsync(id);
            if (entity == null)
            {
                _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruongKCN>().Remove(id);
                return new ApiSuccessResult<bool>() { };
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa");
            }
        }

        public async Task<ApiResult<IPagedList<BaoCaoBaoVeMoiTruongKCNViewModel>>> GetAllPaging(BaoCaoBaoVeMoiTruongKCNRequest request)
        {
            Expression<Func<BaoCaoBaoVeMoiTruongKCN, bool>> filter = x => true;
            if (!string.IsNullOrWhiteSpace(request.FullTextSearch))
            {
                var fullTextSearch = request.FullTextSearch.ToLowerInvariant();
                filter = filter.And(p => p.TenBaoCao.ToLower().Contains(fullTextSearch));
            }

            var data = await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruongKCN>().GetPagedListAsync(predicate: filter, pageIndex: request.PageIndex, pageSize: request.PageSize);
            data.Items.Select(MapEntityToViewModel);
            var result = new PagedList<BaoCaoBaoVeMoiTruongKCNViewModel>();

            result.PageIndex = data.PageIndex;
            result.PageSize = data.PageSize;
            result.IndexFrom= data.IndexFrom;
            result.TotalCount = data.TotalCount;
            result.TotalPages = data.TotalPages;    
            result.Items = data.Items.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IPagedList<BaoCaoBaoVeMoiTruongKCNViewModel>>() { Data = result };
        }

        public Task<ApiResult<BaoCaoBaoVeMoiTruongKCN>> Update(BaoCaoBaoVeMoiTruongKCNViewModel obj)
        {
            throw new NotImplementedException();
        }
        public BaoCaoBaoVeMoiTruongKCNViewModel MapEntityToViewModel(BaoCaoBaoVeMoiTruongKCN entity) {
            var result = new BaoCaoBaoVeMoiTruongKCNViewModel();
            result.IdBaoCaoBaoVeMoiTruongKCN = entity.IdBaoCaoBaoVeMoiTruongKCN;
            result.TenBaoCao = entity.TenBaoCao;
            result.NgayBaoCao = entity.NgayBaoCao != null ? entity.NgayBaoCao.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "";
            result.IdKhuCongNghiep = entity.IdKhuCongNghiep;
            result.TenKhuCongNghiep = entity.KhuCongNghiep.TenKhuCongNghiep;
            return result;
        }
        public BaoCaoBaoVeMoiTruongKCN MapViewModelToEntity(BaoCaoBaoVeMoiTruongKCNViewModel viewModel)
        {
            var entity = new BaoCaoBaoVeMoiTruongKCN();
            entity.IdBaoCaoBaoVeMoiTruongKCN = viewModel.IdBaoCaoBaoVeMoiTruongKCN;
            entity.TenBaoCao = entity.TenBaoCao;
            entity.NgayBaoCao = string.IsNullOrEmpty(viewModel.NgayBaoCao) ? null : DateTime.Parse(viewModel.NgayBaoCao, new CultureInfo("vi-VN"));
            entity.IdKhuCongNghiep = entity.IdKhuCongNghiep;
            return entity;
        }
    }

}
