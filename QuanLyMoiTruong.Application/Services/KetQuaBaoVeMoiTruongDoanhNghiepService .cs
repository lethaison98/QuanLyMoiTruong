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

    public class KetQuaBaoVeMoiTruongDoanhNghiepService : IKetQuaBaoVeMoiTruongDoanhNghiepService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileTaiLieuService _fileTaiLieuService;
        private readonly IDiemQuanTracService _diemQuanTracService;
        private readonly IKhuCongNghiepService _khuCongNghiepService;

        public KetQuaBaoVeMoiTruongDoanhNghiepService(IUnitOfWork unitOfWork, IFileTaiLieuService fileTaiLieuService, IDiemQuanTracService diemQuanTracService, IKhuCongNghiepService khuCongNghiepService)
        {
            _unitOfWork = unitOfWork;
            _fileTaiLieuService = fileTaiLieuService;
            _diemQuanTracService = diemQuanTracService;
            _khuCongNghiepService = khuCongNghiepService;
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var entity = await _unitOfWork.GetRepository<KetQuaBaoVeMoiTruongDoanhNghiep>().FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                await _unitOfWork.SaveChangesAsync();
                return new ApiSuccessResult<bool>() { };
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa");
            }
        }

        public async Task<ApiResult<IList<KetQuaBaoVeMoiTruongDoanhNghiepViewModel>>> GetAll()
        {
            var result = new List<KetQuaBaoVeMoiTruongDoanhNghiepViewModel>();
            var entities = await _unitOfWork.GetRepository<KetQuaBaoVeMoiTruongDoanhNghiep>().GetAllAsync(predicate: x => !x.IsDeleted);
            result = entities.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IList<KetQuaBaoVeMoiTruongDoanhNghiepViewModel>>() { Data = result };
        }

        public async Task<ApiResult<KetQuaBaoVeMoiTruongDoanhNghiepViewModel>> GetById(int id)
        {
            var result = new KetQuaBaoVeMoiTruongDoanhNghiepViewModel();
            var data = await _unitOfWork.GetRepository<KetQuaBaoVeMoiTruongDoanhNghiep>().GetFirstOrDefaultAsync(predicate: x => x.IdKetQuaBaoVeMoiTruongDoanhNghiep == id);
            result = MapEntityToViewModel(data);
            return new ApiSuccessResult<KetQuaBaoVeMoiTruongDoanhNghiepViewModel>() { Data = result };
        }

        public async Task<ApiResult<KetQuaBaoVeMoiTruongDoanhNghiep>> Insert(KetQuaBaoVeMoiTruongDoanhNghiepViewModel obj)
        {
            var entity = new KetQuaBaoVeMoiTruongDoanhNghiep();
            var kcn = await _khuCongNghiepService.GetById(obj.IdKhuCongNghiep);
            obj.TenKhuCongNghiep = kcn.Data.TenKhuCongNghiep;
            entity = MapViewModelToEntity(obj, entity);
            await _unitOfWork.GetRepository<KetQuaBaoVeMoiTruongDoanhNghiep>().InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return new ApiSuccessResult<KetQuaBaoVeMoiTruongDoanhNghiep>() { Data = entity };
        }
        public async Task<ApiResult<bool>> Remove(int id)
        {
            var entity = await _unitOfWork.GetRepository<KetQuaBaoVeMoiTruongDoanhNghiep>().FindAsync(id);
            if (entity != null)
            {
                _unitOfWork.GetRepository<KetQuaBaoVeMoiTruongDoanhNghiep>().Remove(id);
                return new ApiSuccessResult<bool>() { };
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa");
            }
        }

        public async Task<ApiResult<IPagedList<KetQuaBaoVeMoiTruongDoanhNghiepViewModel>>> GetAllPaging(PagingRequest request)
        {
            Expression<Func<KetQuaBaoVeMoiTruongDoanhNghiep, bool>> filter = x => true;
            if (!string.IsNullOrWhiteSpace(request.FullTextSearch))
            {
                var fullTextSearch = request.FullTextSearch.ToLowerInvariant();
                filter = filter.And(p => p.TenKhuCongNghiep.ToLower().Contains(fullTextSearch));
            }
            filter = filter.And(p => !p.IsDeleted);
            var data = await _unitOfWork.GetRepository<KetQuaBaoVeMoiTruongDoanhNghiep>().GetPagedListAsync(predicate: filter, pageIndex: request.PageIndex, pageSize: request.PageSize);
            data.Items.Select(MapEntityToViewModel);
            var result = new PagedList<KetQuaBaoVeMoiTruongDoanhNghiepViewModel>();

            result.PageIndex = data.PageIndex;
            result.PageSize = data.PageSize;
            result.IndexFrom = data.IndexFrom;
            result.TotalCount = data.TotalCount;
            result.TotalPages = data.TotalPages;
            result.Items = data.Items.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IPagedList<KetQuaBaoVeMoiTruongDoanhNghiepViewModel>>() { Data = result };
        }

        public async Task<ApiResult<KetQuaBaoVeMoiTruongDoanhNghiep>> Update(KetQuaBaoVeMoiTruongDoanhNghiepViewModel obj)
        {
            var entity = await _unitOfWork.GetRepository<KetQuaBaoVeMoiTruongDoanhNghiep>().GetFirstOrDefaultAsync(predicate: x => x.IdKetQuaBaoVeMoiTruongDoanhNghiep == obj.IdKetQuaBaoVeMoiTruongDoanhNghiep);
            entity = MapViewModelToEntity(obj, entity);
            _unitOfWork.GetRepository<KetQuaBaoVeMoiTruongDoanhNghiep>().Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return new ApiSuccessResult<KetQuaBaoVeMoiTruongDoanhNghiep>() { Data = entity };
        }
        public async Task<ApiResult<bool>> InsertFromExcel(List<KetQuaBaoVeMoiTruongDoanhNghiepViewModel> list)
        {
            try
            {
                foreach (var obj in list)
                {
                    var kcn = await _khuCongNghiepService.GetById(obj.IdKhuCongNghiep);
                    obj.TenKhuCongNghiep = kcn.Data.TenKhuCongNghiep;
                    var entity = new KetQuaBaoVeMoiTruongDoanhNghiep();
                    entity = MapViewModelToEntity(obj, entity);
                    await _unitOfWork.GetRepository<KetQuaBaoVeMoiTruongDoanhNghiep>().InsertAsync(entity);
                }
                await _unitOfWork.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                return new ApiSuccessResult<bool>() { Data = false };
            }
            return new ApiSuccessResult<bool>() { Data = true };
        }
        public async Task<ApiResult<List<KetQuaBaoVeMoiTruongDoanhNghiepViewModel>>> GetAllByIdBaoCaoBaoVeMoiTruong(int IdBaoCaoBaoVeMoiTruong)
        {
            var result = new List<KetQuaBaoVeMoiTruongDoanhNghiepViewModel>();
            var entities = await _unitOfWork.GetRepository<KetQuaBaoVeMoiTruongDoanhNghiep>().GetAllAsync(predicate: x => x.IdBaoCaoBaoVeMoiTruong == IdBaoCaoBaoVeMoiTruong && !x.IsDeleted);
            result = entities.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<List<KetQuaBaoVeMoiTruongDoanhNghiepViewModel>>() { Data = result };
        }
        public async Task<ApiResult<List<KetQuaBaoVeMoiTruongDoanhNghiepViewModel>>> GetBaoCao2(BaoCaoBaoVeMoiTruongRequest request)
        {
            var result = new List<KetQuaBaoVeMoiTruongDoanhNghiepViewModel>();
            var entities = await _unitOfWork.GetRepository<KetQuaBaoVeMoiTruongDoanhNghiep>().GetAllAsync(predicate: x => !x.IsDeleted, include: x => x.Include(x => x.BaoCaoBaoVeMoiTruong));
            if (request.TuNgay != null)
            {
                entities = entities.Where(x => x.BaoCaoBaoVeMoiTruong.NgayBaoCao >= request.TuNgay).ToList();
            }
            if (request.DenNgay != null)
            {
                entities = entities.Where(x => x.BaoCaoBaoVeMoiTruong.NgayBaoCao <= request.DenNgay).ToList();
            }
            if (request.Nam != 0)
            {
                entities = entities.Where(x => x.BaoCaoBaoVeMoiTruong.Nam == request.Nam).ToList();
            }
            result = entities.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<List<KetQuaBaoVeMoiTruongDoanhNghiepViewModel>>() { Data = result };
        }
        public KetQuaBaoVeMoiTruongDoanhNghiepViewModel MapEntityToViewModel(KetQuaBaoVeMoiTruongDoanhNghiep entity)
        {
            var result = new KetQuaBaoVeMoiTruongDoanhNghiepViewModel();
            result.IdKetQuaBaoVeMoiTruongDoanhNghiep = entity.IdKetQuaBaoVeMoiTruongDoanhNghiep;
            result.IdKhuCongNghiep = entity.IdKhuCongNghiep;
            result.TenKhuCongNghiep = entity.TenKhuCongNghiep;
            result.IdDoanhNghiep = entity.IdDoanhNghiep;
            result.TenDoanhNghiep = entity.TenDoanhNghiep;
            result.SoGiayTo = entity.SoGiayTo;
            result.LoaiHinhSanXuat = entity.LoaiHinhSanXuat;
            result.TongLuongNuocThai = entity.TongLuongNuocThai;
            result.DauNoiVaoHTXLNT = entity.DauNoiVaoHTXLNT? "Có": "Không";
            result.TachDauNoi = entity.TachDauNoi ? "Có" : "Không";
            result.LuongKhiThai = entity.LuongKhiThai;
            result.QuanTracKhiThai = entity.QuanTracKhiThai ? "Có" : "Không";
            result.ChatThaiRanSinhHoat = entity.ChatThaiRanSinhHoat;
            result.ChatThaiRanCongNghiep = entity.ChatThaiRanCongNghiep;
            result.ChatThaiRanNguyHai = entity.ChatThaiRanNguyHai;
            result.TyLeCayXanh = entity.TyLeCayXanh;
            return result;
        }
        public KetQuaBaoVeMoiTruongDoanhNghiep MapViewModelToEntity(KetQuaBaoVeMoiTruongDoanhNghiepViewModel viewModel, KetQuaBaoVeMoiTruongDoanhNghiep entity)
        {
            entity.IdKetQuaBaoVeMoiTruongDoanhNghiep = viewModel.IdKetQuaBaoVeMoiTruongDoanhNghiep;
            entity.IdBaoCaoBaoVeMoiTruong = viewModel.IdBaoCaoBaoVeMoiTruong;
            entity.IdKhuCongNghiep = viewModel.IdKhuCongNghiep;
            entity.TenKhuCongNghiep = viewModel.TenKhuCongNghiep;
            entity.IdDoanhNghiep = viewModel.IdDoanhNghiep;
            entity.TenDoanhNghiep = viewModel.TenDoanhNghiep;
            entity.SoGiayTo = viewModel.SoGiayTo;
            entity.LoaiHinhSanXuat = viewModel.LoaiHinhSanXuat;
            entity.TongLuongNuocThai = viewModel.TongLuongNuocThai;
            entity.DauNoiVaoHTXLNT = viewModel.DauNoiVaoHTXLNT == "Có" ? true : false;
            entity.TachDauNoi = viewModel.TachDauNoi == "Có" ? true : false;
            entity.LuongKhiThai = viewModel.LuongKhiThai;
            entity.QuanTracKhiThai = viewModel.QuanTracKhiThai == "Có" ? true : false;
            entity.ChatThaiRanSinhHoat = viewModel.ChatThaiRanSinhHoat;
            entity.ChatThaiRanCongNghiep = viewModel.ChatThaiRanCongNghiep;
            entity.ChatThaiRanNguyHai = viewModel.ChatThaiRanNguyHai;
            entity.TyLeCayXanh = viewModel.TyLeCayXanh;
            return entity;
        }
    }

}
