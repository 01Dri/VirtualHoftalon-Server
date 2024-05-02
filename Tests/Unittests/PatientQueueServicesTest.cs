using Moq;
using VirtualHoftalon_Server.Exceptions;
using VirtualHoftalon_Server.Models;
using VirtualHoftalon_Server.Models.Dto.PatientQueus;
using VirtualHoftalon_Server.Repositories.Interfaces;
using VirtualHoftalon_Server.Services;
using VirtualHoftalon_Server.Services.Interfaces;
using VirtualHoftalon_Server.Tests.MocksUtils;
using Xunit;

namespace VirtualHoftalon_Server.Tests.Unittests;

public class PatientQueueServicesTest
{

    private Mock<IPatientsQueuesRepository> mockPatientQueueRepo;
    private Mock<IPatientRepository> mockPatientRepo;
    private Mock<IAppointmentRepository> mockAppointmentRepo;
    private PatientQueuesRequestDTO patientQueuesRequestDto;
    private IPatientQueuesService patientQueuesService;
    private Patient mockPatient = MockEntities.GetPatientMock();
    private Appointment mockAppointment = MockEntities.GetAppointmentMock();

    public PatientQueueServicesTest()
    {
        this.mockPatientQueueRepo = new Mock<IPatientsQueuesRepository>();
        this.mockPatientRepo = new Mock<IPatientRepository>();
        this.mockAppointmentRepo = new Mock<IAppointmentRepository>();
        this.patientQueuesRequestDto = new PatientQueuesRequestDTO(1, 1);;
        this.patientQueuesService = new PatientQueuesService(mockPatientQueueRepo.Object, mockPatientRepo.Object, mockAppointmentRepo.Object);;
    }

    [Fact]
    public void TestSavePatientQueue()
    {
        mockPatientRepo.Setup(p => p.GetPatientById(1)).Returns(mockPatient);
        mockAppointmentRepo.Setup(a => a.GetAppointmentById(1)).Returns(mockAppointment);
        mockPatientQueueRepo.Setup(pq =>
                pq.GetLastPositionBySectorAndHour(mockAppointment.Hour, mockAppointment.SectorId))
            .Returns(35);

        var result = patientQueuesService.SavePatient(patientQueuesRequestDto);
        Assert.Equal("P36", result.Password);
        Assert.Equal(36, result.Position);
        Assert.Equal(1, result.PatientId);

    }

    [Fact]
    public void TestFailedToSavePatientQueueOnNullPatient()
    {
        mockPatientRepo.Setup(p => p.GetPatientById(1)).Returns((Patient)null);
        mockAppointmentRepo.Setup(a => a.GetAppointmentById(1)).Returns(mockAppointment);
        mockPatientQueueRepo.Setup(pq =>
                pq.GetLastPositionBySectorAndHour(mockAppointment.Hour, mockAppointment.SectorId))
            .Returns(35);
        
        
        Assert.Throws<NotFoundPatientException>(() => patientQueuesService.SavePatient(patientQueuesRequestDto));
        mockPatientQueueRepo.Verify(p => p.GetLastPositionBySectorAndHour(mockAppointment.Hour, mockAppointment.SectorId), Times.Never);
    }

    [Fact]
    public void TestFailedToSavePatientQueueOnNullAppointment()
    {
        mockPatientRepo.Setup(p => p.GetPatientById(1)).Returns(mockPatient);
        mockAppointmentRepo.Setup(a => a.GetAppointmentById(1)).Returns((Appointment)null);
        mockPatientQueueRepo.Setup(pq =>
                pq.GetLastPositionBySectorAndHour(mockAppointment.Hour, mockAppointment.SectorId))
            .Returns(35);
        Assert.Throws<NotFoundAppointmentException>(() => patientQueuesService.SavePatient(patientQueuesRequestDto));
        mockPatientQueueRepo.Verify(p => p.GetLastPositionBySectorAndHour(mockAppointment.Hour, mockAppointment.SectorId), Times.Never);
    }

    [Fact]
    public void TestFailedToFindLastPositionPatientQueue()
    {
        mockPatientRepo.Setup(p => p.GetPatientById(1)).Returns(mockPatient);
        mockAppointmentRepo.Setup(a => a.GetAppointmentById(1)).Returns(mockAppointment);
        mockPatientQueueRepo.Setup(pq =>
                pq.GetLastPositionBySectorAndHour(mockAppointment.Hour, mockAppointment.SectorId))
            .Returns((int?) null);
        Assert.Throws<NotFoundLastPositionPatientQueue>(() => patientQueuesService.SavePatient(patientQueuesRequestDto));

    }

}