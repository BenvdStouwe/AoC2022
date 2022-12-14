namespace AoC2022;

public class Day3
{
    private const string Alphabet = "abcdefghijklmnopqrstuvwxyz";
    private readonly IReadOnlyDictionary<char, int> CharScore = Alphabet
        .Concat(Alphabet.ToUpper())
        .Select((c, i) => (character: c, score: i + 1))
        .ToDictionary(c => c.character, c => c.score);

    private const string TestInput = @"vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw";

    [Theory]
    [InlineData(TestInput, 157)]
    [InlineData(RealInput, 7742)]
    public void Part1(string input, int expectedResult)
    {
        var result = SolvePart1(input);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(TestInput, 70)]
    [InlineData(RealInput, 2276)]
    public void Part2(string input, int expectedResult)
    {
        var result = SolvePart2(input);

        Assert.Equal(expectedResult, result);
    }

    private int SolvePart1(string input) => 
        input.Split(Environment.NewLine)
            .Select(i => (i[..(i.Length / 2)], i[(i.Length / 2)..]))
            .Select(i => i.Item1.Intersect(i.Item2).First())
            .Sum(c => CharScore[c]);

    private int SolvePart2(string input) => 
        input.Split(Environment.NewLine)
            .Chunk(3)
            .Select(i => i[0].Intersect(i[1]).Intersect(i[2]).First())
            .Sum(c => CharScore[c]);

    private const string RealInput = @"NJvhJcQWTJWTNTFFMTqqGqfTmB
VwVzPldRZVLVRmfsvfjvqfmm
ZDPDHZHVcvDhbvnv
FHHwHBzzVCWWmmCzCPrVmgBwbLTtRFFbbbttRGRLjTcLpbbT
vhZZvdsNSdSMdNvjncppCLcLnGnj
CDZZsNZMZqdNSdlNZCqrzPHDzgrgzwVVWwmwwm
ndlndntsFJntFvccLjjLrjBShcBBfc
GpCGHzVwmmzqQWSSSfWHBhQL
mpCMGGCZVzVwGGVwmJsZnFtZnTSTJtdsvl
nCnPDGmDNmVCsVQDmGSWqvzchWSjjcWGqS
gTnBRLpfTRnrTdZgdLfRdrThvqcvWWhFFWvcFSSgjqqzjv
pfZfTMwrbLTTfsbmQtlVtHHnbs
wNdSdsbTvTZMTvTv
rrdRWdWQhFVdHWBGWQmmmnnMvCfmnhvmCmtZ
rJrVDRWpGddpbSlNSlspPP
chTNrthMMwWMTjfsmRzZszJpwm
BLnFFCngbcBnbbldDlpRjGpmsCzGsGsRGmmG
dqvnvlgbqtcPPMhH
QcLNqZbCzJDQBJJRpwzRpdnRldgnpf
GmmmvVGsHrWffrlwdCWd
CMsFVVFjCmFStGQbbLZNBbJBcTjc
LQVggbQvcLbQLHgvVLhWGGsChssrMWfzGccc
qDnRTTRqJttPfWMChJhGslWlzh
qRTRwPBTBtRZdnjnqqqnQVbjbNLFbbfLgVmgHLQm
cZbzwCwZPlJcMLrNSNfHWNBBNZ
vsQsDCqtsDhmtjVrBNWNjBHrhr
TtDTGnvTlgbbRCGg
BgBlplHlsgNNsJlVpBtPwJhMPRRQSSttRtSP
bvhTnmdFTzddStwStQRddt
ZnZDLvnvqZzbbhFzzmTbnFsVjVlNgsCCNVsVLpNWVgsB
TdptqrrcVGhhzFtw
DRnSfwJlDmmDDVGv
RCSQNSCQZndwbcMqQrBB
wvRlrlwVwwqzgbZRdCJBWfmdzCWfBdhf
cFcsQpNtLLsGTtNGpMdPmDdPBmmBvJPWvDtC
TpjssTFFvLLLcFFQpwbwwHngjHRrZRqZVH
mqqddrPPcPmqPDlrQnjTrbvMvbHzzsjjpTvz
gtBWgGgVhLGWHzMDztzstDHj
hfWRhBBNBGgLNQDPwdNPcPdw
LhQzdhhbTzpMhddhhhTzhnZcBFllHZFtrrHZHMHFjlHr
mwwssqDvjptrvplr
NCSgVDPDwmDgVJVpLfTznQJdhfLhnhQQ
GzjzDhjhhZzcrRgQCBjBPBBjQCgT
vHHHmntsbSgLwbsSmNHbwNbvpqPCBVppCpFTpTPTBtqWBCqV
NJbwNSwdndvmvwhGhgzcfMcDJfgJ
GncgDvvcMGnttjDvrgRRFSZZLZFWdJFJwGQwZBWZ
bPqpChPfsshfZZBdZdLTFZ
lNqqsClmbsNlPbHqPsmblmsrHdvdMngcVrjggvrvggRDcn
bDvtgVVVpMQvjQWmQL
rwTflmlfZJBBdQWQWjQqdM
HsJJmZZwscHrwTrcRbzpcbPgtCSbgz
CsCsRvshMjpbqCqf
ncblgDBgtDmmmTlBgwlgbHHqMFHLqPDMHPHHpqWfFM
TcBctSmTZTtSTzsZvsvJZRsGVb
znznvngttwltzlLwhtThHbqHPvNbNHSSHmmNWHjP
FBcLrRMFQpPqpPSpqHHW
fRQMJZJfrcMcMVrQJJftnwCzVCltgTnstTVnVL
MfLlRfCMrLzRlQgwNqQFcsGd
jtTjjBTvbdqcGjqFcj
vvShDSBDppzhCmzq
plWMptTvfrnncvcRfwqzqLGhzhzThNzNNJqD
jSdSHFPQQbdPCQCssjSbBmhJGNZZNGNqqJNBlJqqLh
VCCCVCQgjdddjCgljCjbbwgRRttgrpftfWrgvpwpnf
MWlbBcPjjvvjPWWMPqgRQZfJZDGGbRZJffQQwh
HrHrnncHpzrJQJfVDQVR
zzsSTtSTLzsspSdtTmHHmpmtFgqcgPlgFqWBqqqBMdWWvFlg
nSqBbJbqlnBBClVZcMgZVgcP
FQwrwHrRwWWFBRPNgNgcCGZZZC
rWFWFTwpwwWzHrnDbfJDLDbBBbbz
BMmNtLMMtFCNFNMvvLmcndpgcdgppPrgrGPPrgJD
WVWWhbTtVnGpjrrPhr
HWssSTHWfRHRsQQFLvfvFFCLCNMNlt
sTmDsQffVrrLCjTFltTFWL
BnwwQBJbJndMMRzMwCLlWlLWWCWLLtRlWF
cqqBMcMqwnznMGzcvDmQhrvssHmPDVssrP
pQGQGJDDrDVJbbfVzvvgPcCZwhZhncscZWWc
SqMMlBBljMmRlchhPTqThCZnPs
FMjMBmjRNFHQJJpHVhVDhG
tHNNdBdNtBBBMgsMpsZm
wVPzVvbwqzhrVqvjqzzsZpDsZDsZmsCPCgZgCM
bVbvLThvvbrWqHmmnJLdHdJQLn
PzTspPZpdLLDZTplPLpPDpvbfhnqNvqzfvNMzQQfNwnQ
GWRHmjmFWMMSnhbhHw
JWWcmtBrBtWBFWGJpsgTgldhLVLpJl
DwLMDzLMhvMcwvgdVqWWlCVgvlqF
TTSBBRpbStHZVgjWFldjRVlV
SnbTBdJBmnpQzMPDMcMznr
nNlMNBPPNtJQnbZhZsgSbh
czzCjcwTdvSbgQNcgNQq
VTdNdGDTzDTdlFFPtBrtLtDr
FMbbfMlzvFsmgVZmmg
SrNTHGmdSQDqLhtQhhgggs
dRDTSDPPcHRdHGDHlwJBbmwljmMcfjbW
sQgWLtqLtWhdqlpNZRpG
blTHTjlvTCJnJvRZdGGhHHGZhFGV
CCDlJclnCmbrmBMgcwcLWtcBsB
vqPWWvqwwCFvFZfZPRFRrcGQrQwsDrNcrwnbDNcQ
LVgJLSBBVtzTLzBMmTMJmLnnDNQcrsGbsQbNbrbDjs
zggVSmmhVdfqFhvHWG
WwdndGGmmmLwwwmRwWSncLRnZqZqhqZthBtqtBqZBgtdtvMH
FfHHzlQQDsFzzrNsVTfttZvTvttTqqtbqb
lQjFDNQFPjCsVCCDjGCwwSGGnccwcHppGp
mrjggcFsFMjdjZRpSZpn
NCqfLCFNbQPzPPlPzNfSRTRZdSdWWwndpqRSSd
vDvzzbPQFNCFtllLLNMBhMcDHGBGMggMmcBc
jhjlBvvnjbtDNPjtSjBDBbDNgHggrQrhghRQrqRrZcRwwqVg
pLdTMsWdLLmpMdqZZdPdVqZgHPwH
WLTCGmMLfPSlbGjlnnJD
gtbwhgHbHgqqbgQthgQLtZZCRjMcjjnRnrRNJmMRJrNhRc
bGWVTTvDvfpVFFBpvvVTdRDMJcrccCrJnMRnNnNCcc
FVWTBsdvdTzTBFWssVQtLgSQtHqqPzPbqHbw
dlzrPTSSjSrllzWhsvVmVtTRTWtf
bJMpLGcqGhNbJQttVQmmvRWWsp
qLbMwqqbGHFGzrlZrjhPHCrj
rNrrffVlqqrfLlPpltcBBTTGRzzZRPRsBTcJ
msbsmWSsMmQwjdMbWMhMhQmcRZRzGjTBGTBcBJBjCHJGcC
FwWbvdhbmrsFrfrgsN
rHjrQHdhdQrvSddcHWLssBSVVpBSWWWWWf
JNfTGtqDwVWBMBMpwM
qlltZgfJFvcRgcRjvc
CqfcwfDqwwmRnnqmRdNRBTRTRrdGdNpTvF
WVbzsZszBbrsvpdMpdQM
tJhbVZHWLLHDgnSwnSSgHB
TZCqqlTsqpZVVsZQJSBSLpLmppnJzmFz
brSgNtGjjRjRRjDddDtrRJcJJbJmmwcmBmnPcJFwFB
jgdRtMjNNjfqlMvShvSZSZ
dJTdqCwMNCgqTQllGBdlGBmmmZ
fcVfVcnbVfrwDLWVfncZBQPlBHRGljLZQjHGQl
brwnnfSFDvfzCTqFzgMJTh
njnsPBjjsrrnGLnbTTjGvcldQPCMllNzMvRQPCdd
ggZgfZtmZVpqZqZWDgFmgqfCcQRcRcWhQcccQddMcvRQdQ
tfqgggVgHpDwDtfwbGLJRjbLjsrLTj
JmrfrmTlDWTfgQCdHCdpqBvQdD
jsZtVzNsSNVQQHnBlVQR
PljljFjPljSsLPtFLTTgTcFrrfMJmrrmrr
hmGcmmndhmGnfmtGnDzFLwrFJQsQFzNFrNJG
ZSqPlSWcWlbgqWVTVWRVZPrjQqjzjFNJzLsNJsLJNqNL
RHcWTZbSMMMPgZcWgSWPPbVMDnBffmtdpDBddfnnvmCdfC
vSJvsbFfJfvqCsTHJswssJnLTZjjhzrrzLrzLMrzhdjM
pBNQDPcpmWDcBNgMMnZPVjdddnndhH
QWlDgmpmgDBlGRgDDgffSqwSwGCwHfvqwSFJ
jvlgvMJclPdGdtdcjMVmMHbFHFVHWHbZHZ
CwhLzLhzQpnqfpfqDVHCHbsbDFZDmHmj
LnBzfQjSzQrPvJvdSSrr
wpcvcsqclDCnVCVvWfnZ
BLRMRtbnbbBLNCjNCjVVZhbC
rFgMPSRnrRpmqpJwqFDs
LZQNQbMrZppLNLQplvlGLNvVmmmfjbwVCfjbwJwCmBCwfj
ShTPRFtTHZPCsnwswsFwCF
WtHRPdThSqZTRtDqtdRWTdpGDLLzrNczvzMGLlQLGDDM
hdcffBvldjhCMljqPwWwWNwWdwqHZr
LtQmbQRVsZQZMZPQSN
tmMRsJMpDhjJzJhv
wNQCMFCDQDBmrHmmRWrrHN
SShLnfqpcqpSZSfrzJvRVrvfrrJH
cRpqdGclpScltTQQtsFQMQsTCT
NCjggZmgfBgnBmgWbcwcTFctcWWfvb
HsDGthRGrtppSQpbFFJTVcJdFbTRvd
rPDGhDDrSzZLtzBLZMCB
RsBBMBsCBlFFCgRsBJzlMjMPNSdPhSrSrzLbmSDrDNmDSd
pZHZZJpGHHHpTTHvTncZqVLdqLbhLrDLdhrSLLbLDDdD
tGtwnJccvCtCffMBgt
wbddvVjfwPhbjjbDbbvbjvTNCNmfHZfpCZRJNzCmJmnJNC
BslcLtclZWsZJWNrRRNRpRmR
BSLBlScGtFMcssMBBFGLlQZTDZQjPddVwwbTdvvdhTZb
NSZHzmLZBnzHmLLzLSntDttDDtddhDtttDWW
QgfjsrrvNNJwtMddcvcvtq
jrfgfQpQrTTVLSNBClFV
GQWcWWPPQRcrJQNDdRcDmmLCFSnqNSmqhCNvFnql
zHfwjzpMjwZmCLqvvnlljC
ZgtVZBtHHZtgQGgPrbPRJdPv
TWdWpJTJTdgLWfWLlLFLrfrgBGsNqhGslBGHqSNqqBNshnws
ZpQmjzbZZCjZCCCPZtttRCCwsBnHNssBHbShsshHqsGBqN
RDRRPpPCzmZCtRpVVJFrfTfWFLLJggJrDv
pDDFlglsvFMgntlTMMqNffmTdfddRM
jhGJLVCHQpHGQCCzLjWdTTdZZdNdcRWNccWfNN
jQjSGjrjCQLhzVSLSCSHGDpngbrnDFtFBwBglBnBvg
wsLzstsgszcpcGLHGpcgcghlDBvQvjQvbFbQCbJBtCCJJv
mnSqRSSqSRThWRnmWWRSJDFTFCFCblbBCFQFCjFj
rZRRWqSSdZZfMVnZLspPsMgHpzMhHGPg
mwHrCLSWWwrsHCHDDsVrsmhfFZFnSSBlFlgZbbgBglbggj
GJdpcRtGJvNRdcPtdpJJdbQZfjfQBlnQBjnBtbfFnB
qcPpqqzFzJqvPVCCmWrVwhrWrz
jjMbvbhDvnRjNRGMmjbMZftSSwwwthJSffStctcwqd
lTQrVlpCVvCcfdcSJqLVcw
srHFWCHrFlrHlrsBsprljjRmDZZnmbDngNBgbNZv
MgTlQJlTQJZWpgLrRssrVqqqpRts
bBNbbzSSjMBPjzhMjsPtRVVRVPRqLttGGs
SjHBbfjNCDfjZgTlZdMJnDJW
lpThgTwtplhghgwhThqnnrdZctSZSjSZcRSRfbdrrc
RBVBGvmBmfdrcvrbbr
PmVGNGmmGRLLQwwLqTnglQ
nHwnBwBTnFHQwRsMhwghmzcm
GtprdCpdtqWdbqbrfdnPPszsWmRzRnShPszS
dGptbCfCrlnVDBJNLDLLVDLQ
CZtCjhTndCzqbCNq
dwpGvpsmwGslDszrNNrzqDMzWMgJ
vmcGccvpBVPTVTjTdTTTdZ
jWZhvZLjZfCZDwrDrSSzJGhVdJccscGsgV
blMBlRqqqgSJLBLcsJ
blmHLmFMMMnRqLmMMFqHmfPDfjQDnCDDQrZvfCjvDr
rnvnHrDLFZmMFLvrHQBMGQggBztzglplRl
sbWWhdNzsshsfhcsjJJPPbWdtQGVGllRTRjRRgBgQlpRlppB
PPCCwNWhPhNfWCzbqmFnDFFnCDLSrvZS
GChNjwWlWJWTJZBggvdgnQgdhdnd
HPsHfHHrpHDpFFrcSfsfpCMmQdntLBMgtmtBgDdLLC
SqpPscpPzpSWzjlCjjCGjl
nvgLvcLgvgvngbLprpJNTDCCRNVJrNPlDDTV
WZsMtsffGQtMzWFqFmWmWsVNJNlDwwCDVRTwJlCCDVLz
BQfGZGmmsMWFstWFmfMsfBccdncbpbSbvbbvHnLbpc
tsmDsvswNZmcZTccfh
zCTpGCbWBRWFWHGRFZJbMbJfnrhnhfMnnZ
TzFGFBRLdpHHNNQddDQDvwQN
fhBBpJgdHddjZQfmVmNzNNLmFN
qvMRrvlbwqlbTTMBMvLssFNmVzzwFDmLLzVD
TRSRWqRRMcBHhGHcdGgPGp
lSjHmtmnpHStblnpSlHSrtmMzLWzqzqCZDDTzTTWqMFqCqVV
sLRLLfPPRQfCTqqVVqFT
dNJgRPNQNsJJhBRvdJvQvNNsjSrrSmrcctpbpHtBrBjLjmSH
nwFwpppjfwSlpLTsqsTgNshhjM
ccBRGvtsmgGNPqNNGP
BCcJHvssdcWBCVmVHSSrZrwVzblpwbzZnf
rcfQRrBPPczjcRBctZDNlnVNHbgZGjVDjN
TvMsFJGSFMhJnNZlwVVnDNTZ
qhSqqmqLCLhFdJLqSvLhmQRQRWcRPczPtzrCrWGRBp
JVhdPhsFPFqLDBHVdHLPvhHDCMwcgJJwbwRgnnCMbwGwcmGC
fzjzpTZTQQQLwCbgGgbMmQcR
jzNpTzfSZtfNSWZlVVtdFFFDHHqLHVqv
TwSNnSnSGVTpNppGlPTlTcVqQrRhVBqdqBRqZqQZqQ
DcDCMfDbCMHJdrRBqbdjRBRZ
gvftMCJHcHfCDmDLgfMmMmmWlwWnWsTTwlGTlWTwppNlGL
pbGMbllDQPhhWWQDpPgVGlMCvRRrQLcCCcfBBQzLBcvQBv
wqnJjSmjrstdqwwFBLcRsBRRszzLFC
qwdddTJTdHtjndqJqHZHmwVWGpDbGTlbWWpWWrPGhhhM
WGllqLjjLCpSffmBmvfpHs
dnrQwZzRTdZwnCThdzzFTVmcBHBJBmsHfBPHcfvcSVHs
QgQrzCdrTRCZzrZLbjGLqNMWGgNNLt
sgPnhPPTTPTTwlJfwNHlqcfs
LMCpFbLLbRpMGbMcCFLVlNlNqrHqVfbHHwNDwr
GjBcCCtWMtMRZTSvgWQTngvg
BCMtJJMpRDlMMvBJBBnfjtcjPhPmZgnhgdcf
NrsrsqFNvrVLVGVrsHsqFgfmcPGdcmhfjdPgfjcnZd
zFTzsNqHqFssLVLQqNTFbsBDwCCwvWlDwRMRCTRBDMDS
zQtLgvggSRtgvVRtLvvnzdnjnGwGdmmrlpnlGz
JssBFpqsDqPNnlWWjrrjqrnj
DHDFBNDfPbJBsFHNMPvpvStQvMRVTtgVTVtv
FvzttFvBTJJzLbvwhCnnVnWwjCnBNC
mQdZgZPDPdPPSsMSQPdZgCwVGmnwnWpGnGhqNWjWCG
ggdDgfQSdcjtFHjlLJfF
ghcgScNNSsCvGSzmpVFlZbrzcFcV
MWWRLRqqqdQwTtLjjmqMlFpFlzVnbFVDwplFzlDr
LHMHqdHWjdQMdMtLHHLtWjJRsGCGSNghmSvPBJBNhsGfvfGP
CbVqqqDbcbMHnnDqcCbrRFCfBvvwGjzrBwQGzrwwBjGwBQ
sTPmpNWdWPTJssSSLPfNljjBvflGtjwwBzMG
mmWgmgSZLTLMZWpnhqZbhFFCnhqnnn
QQmjmZqnmQrfTZlbbcVbBcfbHfzf
vpdSNShNppFdSRtdGBqvJBDlDzqbPPHVBH
tRNSNRFhNpSRhFRMFtGhRGswLZZsZqWnmrmZwqwsTZmmmQ
gGWCllFCGWtGGWdlGlWNZdwpnnSbwpMvpphZpndn
RsshDDLcQVMSJQwJwnvw
HVPzrPcDNhPFGhPC
jtHQGHjGGtdTLjnqTQlmvRPRPBBwRBnFPPWP
hZbzNzVrczZzcbNssVspZZVvBwbmPmJPWmvbBRvPlmvRJF
fzNVDsZMhzpVhpVhlZcMNfcDDdQTLTjGDTCqGCjtSQHdHL
GrbFggGrTrzSrgfwJjdTmwmNJZJd
VMPQplPDptchwdsjmlml
MqMWtBDPPWDWHQtvqQtWPjbzCGLgSBgGbzgrzFgnnz
fcJccCcwcDfcpbRnCfWJnQJqtqtqPQdsGdgPsgTQqg
LSjVMhzSFFrljdNbltNGtgdqQq
MMhSHFFMLzBWDcHHcfcHwb
rwmWtJWMwSNRJMtwNmMrrSsmtTjjlgqnTqZZZPlHnTngTTgn
BGqGqqFBFggjjdGHlj
QDhhLbDQCDFMNcmhRhqJNW
BnRnRvMnLGLSCHvvSnlRfWbbTNQJsJsbNbJTBfQT
tzMmmMwjhcpFjDmMcptrcjzFQggfQPTsWsfgNbbgfhJbPhQT
FdzcrtDwDMtcwtFGRZdRLvdnHRSZZv
HVpsSpvjpNjsBmbGFBnMNnDM
WRRWhZtfrVtLJrBZMnDmDbnZBTGF
thhPLzWzhzwPtLRLWrQlpPvvClcVcCppSvpl
lZPbhnZLRPnnPZZPdlGMBWcBMgMQHBBcvvvzBL
jpFjmwwwCDDbsjvjjgcvQgcNBQ
rbFmppbwhqhGRGZr
ggrLwFgWCBwbMWBbFwLMgNBZdmZHclJPllnJlNRPmSNZRR
ppszzDfhDfhsqpnvDVTfGpSPlPmclHcdRcZmmmdPPGSP
pvtDDVDVpqDfzDfngBLCwQrgCtCwFwrg
pbGjFFGGDjpbsGsmNhNFNRBBBtRhhhHv
JnczJVCvwWJvhPgghgNtNtNJ
nwVSSzdzzqSpvQSZQG
mssLLttQrsMrMzLCRmMmrrSQpvWpDNlBTBDlvNTccDQl
HdHJwJqVPwHnqJwbjJbGjnSgSTWPpNgWWpgBBgcvDWWN
ZHVwVZGwwdndqJVJqfHbGwnwrRLtLMftMvMMRrhmLMthhLmz
RgHGLbTqlZlPRZPHfvvfZttJnvfvjnzr
sVcChDVDccwNhhvjTvVzWJjnzFff
mpNcCMTCGmLqBLGH
wVJwHJHVMtMpBmDDWPQVPWDGDD
zCrlZzCblBvnCDWNGLmvGDLPNG
dqZglgbzrzbbgZqzTFSBHHFJSSSfjjSMfwhj
NMWJSjLMCnHHNMNNHWCHMbVVGBPZTrPVPBVDrBSDGTTr
zvttlFpgdtldwwvftPDPTWQdBZrsrWrGBZ
hFlFmhRFvfCbmWJWHcnj";
}